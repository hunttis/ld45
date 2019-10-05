using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceMagnet : MonoBehaviour
{
    public float magnetStrength = 10.0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Resource>() == null) return;

        var resourceBody = other.GetComponent<Rigidbody>();
        var magnetPosition = transform.position;
        var position = resourceBody.position;
        var distance = Vector3.Distance(magnetPosition, position);
        var forceDirection = (magnetPosition - position).normalized;

        resourceBody.AddForce(forceDirection * (magnetStrength / distance));
    }
}
