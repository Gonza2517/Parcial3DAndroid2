using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string menuScene = "Menu";
    private string gameSceneSingleTap = "MenuAutoNivel1";
    private string gameSceneDoubleTap = "MenuAutoNivel2";

    private bool singleTapDetected = false;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!singleTapDetected)
            {
                singleTapDetected = true;
                Invoke("LoadSingleTapScene", 0.5f);
            }
            else
            {
                LoadDoubleTapScene();
            }
        }
        if (singleTapDetected)
        {
            Invoke("ResetSingleTapDetection", 0.5f);
        }
    }

    void LoadSingleTapScene()
    {
        SceneManager.LoadSceneAsync(gameSceneSingleTap, LoadSceneMode.Single);
        Invoke("UnloadMenuScene", 0.5f);
    }

    void LoadDoubleTapScene()
    {
        SceneManager.LoadSceneAsync(gameSceneDoubleTap, LoadSceneMode.Single);
        Invoke("UnloadMenuScene", 0.5f);
    }

    void UnloadMenuScene()
    {
        if (SceneManager.GetSceneByName(menuScene).isLoaded)
        {
            SceneManager.UnloadSceneAsync(menuScene);
        }
    }

    void ResetSingleTapDetection()
    {
        singleTapDetected = false;
    }
}