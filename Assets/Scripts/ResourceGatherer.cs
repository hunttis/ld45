using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ResourceGatherer : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.GetComponent<Resource>()) return;

        Destroy(other.gameObject);
        // TODO: Give points?
    }
}
