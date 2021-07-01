using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    private Transform target;
    private float speed = 15.0f;
    private bool isHoming;
    private float rocketStrength = 15.0f;
    private float aliveTimer = 5.0f;

    // Update is called once per frame
    void Update()
    {
        if(isHoming && target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += moveDirection * speed * Time.deltaTime;
            transform.LookAt(target);
        }
    }

    /*
        This method takes in a Transform that we will set as the target. 
        It will set the homing boolean to true and then set the GameObject to be destroyed after 
        5 seconds (as defined by aliveTimer).
    */
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        isHoming = true;
        Destroy(gameObject, aliveTimer);
    }

    void OnCollisionEnter(Collision collision)
    {
        /*
            This method first checks if we have a target. 
            If we do, we compare the tag of the colliding object with the tag of the target. 
            If they match, we get the rigidbody of the target. 
            We then use the normal of the collision contact to determine which direction to push the target in. 
            Finally we apply the force to the target and destroy the missile.
        */
        if(target != null)
        {
            if(collision.gameObject.CompareTag(target.tag))
            {
                Rigidbody targetRb = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 away = -collision.GetContact(0).normal;
                targetRb.AddForce(away * rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
