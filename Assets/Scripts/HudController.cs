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
    public GameObject fuelBarPanel;
    public RawImage fuelBarImage;
    public List<RawImage> lives;
    public bool isMineBuildable;

    private GameController _gameController;
    private PlayerController _playerController;

    private void Start()
    {
        _gameController = uiContainerController.gameController;
        _playerController = FindObjectOfType<PlayerController>();
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

        fuelBarPanel.SetActive(_playerController._jetPackFuel < _playerController._jetPackFuelMax);
        fuelBarImage.transform.localScale = new Vector3(
            Math.Max(0, _playerController._jetPackFuel / _playerController._jetPackFuelMax),
            1,
            1
        );

        Debug.Log(lives.Count);
        for (var i = 0; i < 3; i++)
        {
            lives[i].enabled = _playerController.lives > i;
        }
    }
}
