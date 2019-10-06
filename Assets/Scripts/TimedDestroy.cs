using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestroy : MonoBehaviour
{

    public float timeUntilDestroy;

    void Update()
    {
        timeUntilDestroy -= Time.deltaTime;
        if (timeUntilDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }
}
