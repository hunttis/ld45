using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    public HudContainerController uiContainerController;
    public Text resourcesText;
    public GameObject buildMinePanel;
    public bool isMineBuildable;

    private GameController _gameController;

    private void Start()
    {
        _gameController = uiContainerController.gameController;
    }

    void Update()
    {
        resourcesText.text = String.Join(
            "\n",
            _gameController.maxResourceAmountMap.Keys.Select(
                type => $"{type.ToUpper()}: {_gameController.collectedResourceAmounts[type]} / {_gameController.maxResourceAmountMap[type]}"
            )
        );

        buildMinePanel.SetActive(isMineBuildable);
    }
}
