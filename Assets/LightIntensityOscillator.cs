using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightIntensityOscillator : MonoBehaviour
{
    public Light directionalLight; // Assign your directional light in the inspector
    public float cycleDuration = 2f; // Duration of one complete cycle (0 to 2 and back to 0)

    private float elapsedTime = 0f;

    void Update()
    {
        if (directionalLight != null)
        {
            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate the current cycle position (0 to 1)
            float cyclePosition = (elapsedTime % cycleDuration) / cycleDuration;

            // Calculate the light intensity (0 to 2 and then smoothly back to 0)
            float intensity;
            if (cyclePosition <= 0.5f)
            {
                intensity = cyclePosition * 4f; // Increase from 0 to 2
            }
            else
            {
                intensity = (1f - cyclePosition) * 4f; // Decrease from 2 to 0
            }

            // Apply the calculated intensity to the light
            directionalLight.intensity = intensity;
        }
    }
}