using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardManager : MonoBehaviour
{
    public Text scoreboardText; 
    private List<float> times = new List<float>();

    void Start()
    {
        UpdateScoreboard();
    }

    public void AddTime(float time)
    {
       
        times.Add(time);
        UpdateScoreboard();
    }

    void UpdateScoreboard()
    {
        
        if (scoreboardText != null)
        {
            string newText = "";
            for (int i = 0; i < times.Count; i++)
            {
                newText += "Lap " + (i + 1) + ": " + FormatTime(times[i]) + "\n";
            }
            scoreboardText.text = newText;

            Canvas.ForceUpdateCanvases();
        }
    }

    string FormatTime(float timeInSeconds)
    {
        int minutes = Mathf.FloorToInt(timeInSeconds / 60f);
        int seconds = Mathf.FloorToInt(timeInSeconds % 60f);
        float milliseconds = (timeInSeconds * 1000f) % 1000f;

        return string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
    }
}