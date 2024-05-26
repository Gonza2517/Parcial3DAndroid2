using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownPickup : MonoBehaviour
{
    public float deaccelerationAmount = 10f; // Cantidad deaceleración
    public float duration = 5f; // Duración
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
            rb.velocity += player.transform.forward * -deaccelerationAmount; // Deaceleración
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