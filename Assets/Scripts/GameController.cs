using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
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
    }

    public void CollectResource(string type)
    {
        collectedResourceAmounts[type] += 1;

        var collectedResourceTypeCount = maxResourceAmounts.Count(m => collectedResourceAmounts[m.type] == m.amount);
        if (collectedResourceTypeCount == maxResourceAmounts.Count)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
