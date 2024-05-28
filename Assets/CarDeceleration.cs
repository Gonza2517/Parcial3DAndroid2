using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDeceleration : MonoBehaviour
{
    public float decelerationRate = 5f; // The rate at which the car will decelerate
    private Rigidbody rb;
    private bool isDecelerating = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DecelerationZone"))
        {
            isDecelerating = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DecelerationZone"))
        {
            isDecelerating = false;
        }
    }

    void FixedUpdate()
    {
        if (isDecelerating)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, decelerationRate * Time.deltaTime);
        }
    }
}