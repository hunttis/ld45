using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnTryAgainClick()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("currentWorldScene", "World 00"));
    }

    public void OnGiveUpClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
