using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    public GameObject resourceObject;
    public float spawnRate;

    void Start()
    {
        InvokeRepeating(nameof(GenerateResources), 0.0f, spawnRate);
    }

    void Update()
    {

    }

    void GenerateResources()
    {
        var resource = Instantiate(
            resourceObject,
            transform.position + transform.right * 2.0f,
            Quaternion.identity
        );
        var body = resource.GetComponent<Rigidbody>();

        body.AddForce(transform.right * 100.0f);
    }
}
