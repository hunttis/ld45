using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashShipController : MonoBehaviour
{
    private Rigidbody _body;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _body.AddForce(transform.forward * 500.0f);
    }
}
