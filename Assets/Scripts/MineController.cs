using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MineController : MonoBehaviour
{
    public GameObject resourceObject;
    public float spawnRate;

    private void Start()
    {
        InvokeRepeating(nameof(GenerateResources), 0.0f, spawnRate);
    }

    private void GenerateResources()
    {
        var resource = Instantiate(
            resourceObject,
            transform.position + Vector3.up * 2.5f,
            Quaternion.identity
        );

        var body = resource.GetComponent<Rigidbody>();
        body.AddForce(randomVector().normalized * 10.0f);

    }

    private Vector3 randomVector()
    {
        return new Vector3(Random.value * 2 - 1, Random.value * 2 - 1, Random.value * 2 - 1);
    }
}
