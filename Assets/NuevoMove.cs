using System.Collections;
using UnityEngine;

public class GyroCarController : MonoBehaviour
{
    public float sensitivity = 0.5f; 
    public float forwardSpeed = 100.0f;
    public float accelerationForce = 50.0f;
    public float reverseSpeed = 50.0f;
    public float holdThreshold = 0.5f;
    public float doubleTapTime = 0.3f;
    public float steeringSmoothTime = 0.2f; 
    public Transform cameraTransform; 

    private Rigidbody rb;
    private Quaternion initialRotation;
    private bool isForward = true;
    private float lastTapTime;
    private float tapStartTime;
    private float targetAngle;
    private float currentAngle;
    private float angleVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.drag = 0.5f;
        rb.angularDrag = 0.5f;

        Input.gyro.enabled = true;
        initialRotation = Quaternion.Euler(90f, 0f, 0f);
    }

    void Update()
    {
        Vector3 gyroInput = Input.gyro.rotationRateUnbiased;

        float rotationZ = gyroInput.z * sensitivity;

        targetAngle += rotationZ;
        currentAngle = Mathf.SmoothDampAngle(currentAngle, targetAngle, ref angleVelocity, steeringSmoothTime);
    
        Quaternion cameraRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = cameraRotation * Quaternion.Euler(0, currentAngle, 0);

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                tapStartTime = Time.time;
                if (Time.time - lastTapTime < doubleTapTime)
                {
                    isForward = !isForward;
                }
                lastTapTime = Time.time;
            }
            else if (touch.phase == TouchPhase.Stationary)
            {
                if (Time.time - tapStartTime > holdThreshold)
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }

        if (isForward)
        {
            float currentSpeed = Vector3.Dot(rb.velocity, transform.forward);
            float remainingForce = Mathf.Clamp(forwardSpeed - currentSpeed, 0, accelerationForce);
            rb.AddForce(transform.forward * remainingForce * Time.deltaTime, ForceMode.VelocityChange);
        }
        else
        {
            float currentSpeed = Vector3.Dot(rb.velocity, -transform.forward);
            float remainingForce = Mathf.Clamp(reverseSpeed - currentSpeed, 0, accelerationForce);
            rb.AddForce(-transform.forward * remainingForce * Time.deltaTime, ForceMode.VelocityChange);
        }
    }

    void ResetGyroRotation()
    {
        transform.rotation = initialRotation;
    }
}