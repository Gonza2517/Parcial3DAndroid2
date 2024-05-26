using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class GyroCarController : MonoBehaviour
{
    public float sensitivity = 1.0f, forwardSpeed = 100.0f, accelerationForce = 50.0f, reverseSpeed = 50.0f, holdThreshold = 0.5f, GyroMin = -1.0f, GyroMax = 1.0f;
    private Rigidbody rb;
    private Quaternion initialRotation;
    private bool isForward = true;
    private float doubleTapTime = 0.3f, lastTapTime, tapStartTime;

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Rigidbody
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.drag = 0.5f;
        rb.angularDrag = 0.5f;

        Input.gyro.enabled = true;
        initialRotation = Quaternion.Euler(90f, 0f, 0f);  // Establecer Gyro en 0
    }

    void Update()
    {
        Vector3 gyroInput = Input.gyro.rotationRateUnbiased;
        // Clamp the Y-axis rotation
        gyroInput.z = Mathf.Clamp(gyroInput.z, GyroMin, GyroMax);

        float steering = Mathf.Lerp(0, -gyroInput.z * sensitivity, Time.deltaTime * 5);
        transform.Rotate(Vector3.up, steering);
        float clampedGyroY = Mathf.Clamp(gyroInput.z, GyroMin, GyroMax); // Limites Gyroscopio
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Detectar toques
            if (touch.phase == TouchPhase.Began)
            {
                tapStartTime = Time.time;
                if (Time.time - lastTapTime < doubleTapTime)
                {
                    isForward = !isForward; // Cambiar sentido con double tap
                }
                lastTapTime = Time.time;
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                if (Time.time - tapStartTime > holdThreshold)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero; // Frenar al holdear
                }
            }
        }

        if (isForward)
        {
            float currentSpeed = Vector3.Dot(rb.velocity, transform.forward);
            float remainingForce = Mathf.Clamp(forwardSpeed - currentSpeed, 0, accelerationForce);
            rb.AddForce(transform.forward * remainingForce * Time.deltaTime, ForceMode.VelocityChange); // Acelerar
        }
        else
        {
            float currentSpeed = Vector3.Dot(rb.velocity, -transform.forward);
            float remainingForce = Mathf.Clamp(reverseSpeed - currentSpeed, 0, accelerationForce);
            rb.AddForce(-transform.forward * remainingForce * Time.deltaTime, ForceMode.VelocityChange); // Retroceder
        }
    }

    void ResetGyroRotation()
    {
        transform.rotation = initialRotation;
    }
}