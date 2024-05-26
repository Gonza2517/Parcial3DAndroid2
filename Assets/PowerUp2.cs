using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownPickup : MonoBehaviour
{
    public float deaccelerationAmount = 10f; // Cantidad deaceleraci�n
    public float duration = 5f; // Duraci�n
    private bool isActivated = false;
    private float timer = 0f; // Tiempo activado

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplyPowerUpEffect(other.gameObject);
            Destroy(gameObject);
        }
    }

    void ApplyPowerUpEffect(GameObject player)
    {
        isActivated = true;
        timer = 0f; // Empezar a contar el tiempo
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity += player.transform.forward * -deaccelerationAmount; // Deaceleraci�n
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            if (timer >= duration)
            {
                isActivated = false; // Desactivarlo
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}