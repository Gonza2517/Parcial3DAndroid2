using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardManager : MonoBehaviour
{
    public Text scoreboardText; // Assign this in the Unity Inspector
    private List<float> times = new List<float>();

    void Start()
    {
        UpdateScoreboard();
    }

    public void AddTime(float time)
    {
        Debug.Log("Adding time: " + time); // Debug log to verify AddTime is called
        times.Add(time);
        UpdateScoreboard();
    }

    void UpdateScoreboard()
    {
        Debug.Log("Updating scoreboard with " + times.Count + " times"); // Debug log to verify UpdateScoreboard is called
        if (scoreboardText != null)
        {
            string newText = "";
            for (int i = 0; i < times.Count; i++)
            {
                newText += "Lap " + (i + 1) + ": " + FormatTime(times[i]) + "\n";
            }
            scoreboardText.text = newText;
            Debug.Log("Scoreboard text updated to: " + newText); // Log the updated text

            // Force the Canvas to update
            Canvas.ForceUpdateCanvases();
        }
        else
        {
            Debug.LogError("scoreboardText is not assigned!"); // Log an error if the Text component is not assigned
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