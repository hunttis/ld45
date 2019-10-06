using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleteScript : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnExitClick()
    {
        Application.Quit();
    }
}
