using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public GameController gameController;
    private Canvas _canvas;
    private Text _metalText;

    void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _metalText = _canvas.GetComponent<Text>();
    }

    void Update()
    {
        _metalText.text = $"Metal: {gameController.currentMetalAmount} / {gameController.maxMetalAmount}";
    }
}
