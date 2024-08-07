using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer1 : MonoBehaviour
{
    public Text timerText;

    private bool Terminaste = false;
    private float startTime;

    private void Start()
    {
        startTime = Time.time;
    }
    void Update()
    {
        if (Terminaste)
        {
            timerText.color = Color.green;
            return;
        }
        else
        {
            float TiempoActual = Time.time - startTime;
            string minutes = ((int)TiempoActual / 60).ToString();
            string seconds = (TiempoActual % 60).ToString("f2");
            timerText.text = minutes + " : " + seconds;
        }
    }
}
