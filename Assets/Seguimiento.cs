using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCarPathFollowing : MonoBehaviour
{
    public float maxSpeed = 10f; 
    public float acceleration = 5f; 
    public float decelerationDistance = 5f; 
    public float steeringAngle = 30f; 
    public float steerDamping = 0.1f; 
    public float targetReachedThreshold = 1.0f; 
    private List<Transform> checkpoints; // Lista para guardar los checkpoints
    private int currentCheckpointIndex = 0; // Index de los checkpoints
    private Rigidbody rb;

    void Start()
    {
        GameObject checkpointParent = GameObject.Find("Checkpoints"); // Tomar lista de Checkpoints del GameObject "Checkpoints"

        checkpoints = new List<Transform>();
        foreach (Transform child in checkpointParent.transform)
        {
            checkpoints.Add(child);
        }
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb != null && checkpoints.Count > 0)
        {
            Transform targetCheckpoint = checkpoints[currentCheckpointIndex];
            Vector3 direction = (targetCheckpoint.position - transform.position).normalized;
            float distanceToCheckpoint = Vector3.Distance(transform.position, targetCheckpoint.position);
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, steeringAngle * Mathf.Deg2Rad * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDirection), steerDamping);
            float currentAcceleration = acceleration;

            if (distanceToCheckpoint < decelerationDistance)
            {
                currentAcceleration = Mathf.Lerp(0f, acceleration, distanceToCheckpoint / decelerationDistance); // Reducir velocidad al acercarse al checkpoint
            }
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.AddForce(transform.forward * currentAcceleration, ForceMode.Acceleration); // Aplicar velocidad continua
            }
            if (distanceToCheckpoint < targetReachedThreshold) // Check por si llegó al checkpoint
            {
                Destroy(targetCheckpoint.gameObject);
                currentCheckpointIndex = (currentCheckpointIndex + 1) % checkpoints.Count; // Actualizar el index al siguiente checkpoint
            }
        }
    }
}