using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAsteroidController : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0.02f, 0.0f, 0.02f);
    }
}
