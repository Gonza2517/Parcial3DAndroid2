using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityOscillator : MonoBehaviour
{
    public Light directionalLight; 
    public float cycleDuration = 2f; 

    private float elapsedTime = 0f;

    void Update()
    {
        if (directionalLight != null)
        {
            elapsedTime += Time.deltaTime;
            float cyclePosition = (elapsedTime % cycleDuration) / cycleDuration;

            float intensity;
            if (cyclePosition <= 0.5f)
            {
                intensity = cyclePosition * 4f; 
            }
            else
            {
                intensity = (1f - cyclePosition) * 4f; 
            }
            directionalLight.intensity = intensity;
        }
    }
}