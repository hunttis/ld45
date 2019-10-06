using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceNode : MonoBehaviour
{
    public GameObject resourceType;

    private bool _isBuilt;

    private void OnTriggerStay(Collider other)
    {
        if (_isBuilt || !Input.GetKeyDown(KeyCode.E) || other.GetComponent<PlayerController>() == null) return;

        var node = transform;
        var mine = Instantiate(Resources.Load<MineController>("Mine"), node.position, node.rotation);
        mine.resourceObject = resourceType;
        mine.spawnRate = 1.0f;
        _isBuilt = true;
    }
}
