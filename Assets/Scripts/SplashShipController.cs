using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashShipController : MonoBehaviour
{
    private Rigidbody _body;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        _body.AddForce(transform.forward * 500.0f);
    }
}
