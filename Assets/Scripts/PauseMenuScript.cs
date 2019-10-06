using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    private GameController _gameController;

    void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    public void OnContinueClick()
    {
        _gameController.TogglePause();
    }

    public void OnGiveUpClick()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
