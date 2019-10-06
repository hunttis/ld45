using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [Serializable]
    public struct MaxResourceAmount
    {
        public string tag;
        public int amount;
    }

    public List<MaxResourceAmount> maxResourceAmounts;
    public Dictionary<string, int> maxResourceAmountMap;
    public Dictionary<string, int> collectedResourceAmounts;

    void Awake()
    {
        collectedResourceAmounts = maxResourceAmounts.ToDictionary(r => r.tag, r => 0);
        maxResourceAmountMap = maxResourceAmounts.ToDictionary(r => r.tag, r => r.amount);
    }

    public void CollectResource(string tag)
    {
        collectedResourceAmounts[tag] += 1;

        var collectedResourceTypeCount = maxResourceAmounts.Count(m => collectedResourceAmounts[m.tag] == m.amount);
        if (collectedResourceTypeCount == maxResourceAmounts.Count)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
