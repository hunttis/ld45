using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public UiContainerController uiContainerController;
    public Text resourcesText;

    private GameController _gameController;

    private void Start()
    {
        _gameController = uiContainerController.gameController;
    }

    void Update()
    {
        resourcesText.text = String.Join(
            "\n",
            _gameController.maxResourceAmounts.Keys.Select(
                tag => $"{tag}: {_gameController.collectedResourceCounts[tag]} / {_gameController.maxResourceAmounts[tag]}"
            )
        );
    }
}
