using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Corruption : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        var corruptable = other.GetComponent<Corruptable>();
        if (corruptable == null) return;
        corruptable.Corrupt(Time.deltaTime);
    }
}
