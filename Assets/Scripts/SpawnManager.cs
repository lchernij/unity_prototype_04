using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemiesPrefab;
    public GameObject[] powerupPrefabs;
    private float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waveNumber);
        SpawnPowerup();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            SpawnEnemyWave(++waveNumber);
            SpawnPowerup();
        }
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemiesPrefab.Length);
            Instantiate(enemiesPrefab[randomEnemy], GenerateSpawnPosition(), enemiesPrefab[randomEnemy].transform.rotation);
        }
    }

    void SpawnPowerup()
    {
        int randomPowerUp = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerUp], GenerateSpawnPosition(), powerupPrefabs[randomPowerUp].transform.rotation);
    }

    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        return new Vector3(spawnPosX, 0, spawnPosZ);
    }
}
