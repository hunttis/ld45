using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceGatherer : MonoBehaviour
{
    private GameController _gameController;

    void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var resource = other.gameObject.GetComponent<Resource>();
        if (!resource) return;

        _gameController.CollectResource(resource.GetResourceType());
        Destroy(other.gameObject);
    }
}
