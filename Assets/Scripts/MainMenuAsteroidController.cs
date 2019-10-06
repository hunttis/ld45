using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAsteroidController : MonoBehaviour
{
    private Rigidbody _body;

    void Start()
    {
        _body = GetComponent<Rigidbody>();
        // _body.AddForce(transform.up * 500.0f);
        _body.angularVelocity = new Vector3(0.02f, 0.02f, 0.02f);
    }
}
