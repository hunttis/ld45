using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
    
public class FireController : MonoBehaviour
{
    public GameObject resourceObject;
    public Vector3 direction; // should be a unit vector
    public float fireInterval;
    public float velocity;
    
    void Start()
    {
        InvokeRepeating(nameof(GenerateProjectiles), 0.0f, fireInterval);
    }

    private void Update()
    {
    }

    void GenerateProjectiles()
    {
        var resource = Instantiate(
            resourceObject,
            transform.position + direction * 0.1f,
            Quaternion.identity
        );
        var body = resource.GetComponent<Rigidbody>();

        body.AddForce(direction * velocity);

    }
}