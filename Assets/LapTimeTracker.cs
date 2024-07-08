using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LapTimeTracker : MonoBehaviour
{
    public ScoreboardManager scoreboardManager;
    private float startTime;
    private int lapCount = 0;
    private bool isTiming = false;
    private int totalLaps = 3; 

    void Start()
    {
        StartRace();
    }

    void OnTriggerEnter(Collider other)
    {
       
        if (isTiming && other.CompareTag("Player"))
        {
            CompleteLap();
        }
    }

    void StartRace()
    {
        lapCount = 0;
        isTiming = true;
        startTime = Time.time;
     
    }

    void CompleteLap()
    {
        float lapTime = Time.time - startTime;
        lapCount++;
        Debug.Log("Lap " + lapCount + " Time: " + lapTime + "s");
        scoreboardManager.AddTime(lapTime);

        startTime = Time.time;

        if (lapCount >= totalLaps)
        {
            isTiming = false;
            Invoke("DelayedAction", 5f);
        }
    }
    void DelayedAction()
    {
        SceneManager.LoadSceneAsync("Menu", LoadSceneMode.Single);
    }
}