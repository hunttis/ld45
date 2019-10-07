using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject pauseMenu;
    public String nextScene;

    [Serializable]
    public struct MaxResourceAmount
    {
        public string type;
        public int amount;
    }

    public List<MaxResourceAmount> maxResourceAmounts;
    public Dictionary<string, int> maxResourceAmountMap;
    public Dictionary<string, int> collectedResourceAmounts;

    void Awake()
    {
        collectedResourceAmounts = maxResourceAmounts.ToDictionary(r => r.type, r => 0);
        maxResourceAmountMap = maxResourceAmounts.ToDictionary(r => r.type, r => r.amount);
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void CollectResource(string type)
    {
        collectedResourceAmounts[type] = Math.Min(collectedResourceAmounts[type] + 1, maxResourceAmountMap[type]);

        var collectedResourceTypeCount = maxResourceAmounts.Count(m => collectedResourceAmounts[m.type] >= m.amount);
        if (collectedResourceTypeCount >= maxResourceAmounts.Count)
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    public void TogglePause()
    {
        if (Time.timeScale > 0.0f)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
        }
    }
}
