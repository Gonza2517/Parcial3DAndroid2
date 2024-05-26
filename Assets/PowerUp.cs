using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccelerationPowerUp : MonoBehaviour
{
    public float accelerationAmount = 10f; // Cantidad de aceleración
    public float duration = 5f; // Duración del poder
    private bool isActivated = false;
    private float timer = 0f; // Contador tiempo activado

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Identificar si el GO tiene el tag Player
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
            rb.velocity += player.transform.forward * accelerationAmount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivated)
        {
            if (timer >= duration) // Comparación para finalizar el poder
            {
                isActivated = false;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }
}