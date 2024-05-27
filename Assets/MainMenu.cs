using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        SceneManager.UnloadSceneAsync("PistaCarrera");
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ChangeScene();
        }
    }

    void ChangeScene()
    {
        Scene currentLoadedScene = SceneManager.GetSceneByName("Menu");

        if (currentLoadedScene.isLoaded)
        {
            SceneManager.LoadScene("PistaCarrera", LoadSceneMode.Single);
            SceneManager.UnloadSceneAsync("Menu");
        }
    }
}