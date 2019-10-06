using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene("SplashScene");
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
