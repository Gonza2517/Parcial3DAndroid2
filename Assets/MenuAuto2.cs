using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuAuto2 : MonoBehaviour
{
    private string menuScene = "MenuAuto2";
    private string gameSceneSingleTap = "PistaCarreraNivel2Subaru";
    private string gameSceneDoubleTap = "PistaCarrera2";

    private bool singleTapDetected = false;

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
        Scene menuSceneObject = SceneManager.GetSceneByName(menuScene);
        if (menuSceneObject.IsValid() && menuSceneObject.isLoaded)
        {
            SceneManager.UnloadSceneAsync(menuScene);
        }
    }

    void ResetSingleTapDetection()
    {
        singleTapDetected = false;
      
    }
}