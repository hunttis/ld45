using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public GameObject resourceType;

    private HudController _hudController;
    private bool _isBuilt;

    private void Start()
    {
        _hudController = FindObjectOfType<HudController>();
    }

    void OnTriggerStay(Collider other)
    {
        if (_isBuilt) return;

        if (_hudController)
        {
            _hudController.isMineBuildable = true;
        }

        if (!Input.GetKeyDown(KeyCode.E) || other.GetComponent<PlayerController>() == null) return;

        var node = transform;
        var mine = Instantiate(Resources.Load<MineController>("Mine"), node.position, node.rotation);
        mine.resourceObject = resourceType;
        mine.spawnRate = 1.0f;
        _isBuilt = true;
    }

    void OnTriggerExit()
    {
        if (_hudController)
        {
            _hudController.isMineBuildable = false;
        }
    }
}
