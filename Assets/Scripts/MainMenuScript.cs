﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void OnPlayClick()
    {
        SceneManager.LoadScene("World 01", LoadSceneMode.Single);
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}