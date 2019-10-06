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
        collectedResourceAmounts[type] = Math.Min(collectedResourceAmounts[type] + 1, maxResourceAmountMap[type]);

        var collectedResourceTypeCount = maxResourceAmounts.Count(m => collectedResourceAmounts[m.type] >= m.amount);
        Debug.Log("collectedResourceTypeCount = " + collectedResourceTypeCount);
        Debug.Log("maxResourceAmounts.Count = " + maxResourceAmounts.Count);
        if (collectedResourceTypeCount >= maxResourceAmounts.Count)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
