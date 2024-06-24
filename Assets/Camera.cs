using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asphalt8Camera : MonoBehaviour
{
    public Transform target; // The target to follow (player's car)
    public Vector3 offset = new Vector3(0f, 2f, -6f); // Offset from the target's rear
    public float followSpeed = 5f; // Speed at which the camera follows the target
    public float lookAtRotationSpeed = 5f; // Speed of camera rotation towards the target's forward direction
    public float minLookAheadDistance = 5f; // Minimum distance to look ahead based on car speed
    public float maxLookAheadDistance = 15f; // Maximum distance to look ahead based on car speed
    public float lookAheadSpeedMultiplier = 0.1f; // Multiplier for adjusting look ahead based on car speed
    public float rotationDamping = 2f; // Damping for camera rotation
    public float shakeMagnitude = 0.1f; // Magnitude of camera shake during high-speed maneuvers
    public float shakeSpeed = 5f; // Speed of camera shake
    public float shakeReductionFactor = 2f; // Reduction factor for camera shake

    private Vector3 targetPosition;
    private Vector3 smoothVelocity;
    private Vector3 rotationSmoothVelocity;
    private float currentLookAheadDistance;
    private float currentRotationAngle;
    private float targetRotationAngle;
    private float shakeIntensity;

    void Start()
    {
        if (target == null)
        {
            enabled = false;
        }
        currentLookAheadDistance = minLookAheadDistance;
    }

    void FixedUpdate()
    {
        if (target != null)
        {     
            targetPosition = target.position - target.forward * offset.z + target.up * offset.y;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref smoothVelocity, 1f / followSpeed);

            float carSpeed = target.GetComponent<Rigidbody>().velocity.magnitude;
            float lookAheadFactor = Mathf.Clamp01(carSpeed * lookAheadSpeedMultiplier);
            currentLookAheadDistance = Mathf.Lerp(minLookAheadDistance, maxLookAheadDistance, lookAheadFactor);

            Vector3 lookAtPosition = target.position + target.forward * currentLookAheadDistance;

            Vector3 targetForward = lookAtPosition - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetForward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * lookAtRotationSpeed);

            shakeIntensity = Mathf.Lerp(shakeIntensity, 0f, Time.deltaTime * shakeReductionFactor);
            if (carSpeed > 20f) 
            {
                shakeIntensity += shakeMagnitude * shakeSpeed * Time.deltaTime;
            }

            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
            shakeOffset.z = 0; 
            transform.position += shakeOffset;
        }
    }
}