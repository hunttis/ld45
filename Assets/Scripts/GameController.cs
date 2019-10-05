using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Dictionary<string, int> collectedResourceCounts = new Dictionary<string, int>()
    {
        {"Metal", 0},
        {"Another", 0},
    };

    public Dictionary<string, int> maxResourceAmounts = new Dictionary<string, int>()
    {
        {"Metal", 5},
        {"Another", 3}
    };

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (collectedResourceCounts["Metal"] >= maxResourceAmounts["Metal"])
        {
            Debug.Log("🎉");
        }
    }
}
