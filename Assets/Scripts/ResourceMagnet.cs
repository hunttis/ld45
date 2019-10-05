using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class ResourceMagnet : MonoBehaviour
{
    public float maxDistance = 10.0f;
    public float magnetStrength = 10.0f;

    private Rigidbody[] _resources;

    private void Awake()
    {
        InvokeRepeating(nameof(UpdateResourceList), 0, 1);
    }

    private void UpdateResourceList()
    {
        _resources = GameObject.FindGameObjectsWithTag("Resource").AsEnumerable()
            .Select(obj => obj.GetComponent<Rigidbody>()).ToArray();
    }

    private void FixedUpdate()
    {
        var magnetPosition = transform.position;
        foreach (var resource in _resources)
        {
            var position = resource.position;
            var distance = Vector3.Distance(magnetPosition, position);
            if (distance > maxDistance) continue;

            var forceDirection = (magnetPosition - position).normalized;
            resource.AddForce(forceDirection * (magnetStrength / distance));
        }
    }
}
