using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireController : MonoBehaviour
{
    public GameObject resourceObject;
    public Vector3 direction; // should be a unit vector
    public float fireInterval;
    public float velocity;
    
    private const float Tilt = 0.1f;
    private const int NormN = 6; // Quality of the random numbers; the higher the better
    
    void Start()
    {
        direction = direction.normalized;
        InvokeRepeating(nameof(GenerateProjectiles), 0.0f, fireInterval);
    }

    private void Update()
    {
    }

    void GenerateProjectiles()
    {
        var resource = Instantiate(
            resourceObject,
            transform.position + direction,
            Quaternion.identity
        );
        var body = resource.GetComponent<Rigidbody>();

        body.AddForce(direction * velocity);

        var theta = NormRand(Tilt);
        var tau = NormRand(Tilt);
        direction = Quaternion.Euler(0, theta, 0) * direction;

        var dir2D = Vector3.ProjectOnPlane(direction, Vector3.up);
        
        direction = Quaternion.AngleAxis(tau, 
                        Vector3.Cross(dir2D, Vector3.up)) * direction;
    }

    // Return approximately normally distributed random number (Irwin–Hall distribution)
    private static float NormRand(float variance)
    {
        var rand = -0.5f * NormN;
        for (var i = 0; i < NormN; i++)
        {
            rand += Random.value;
        }

        return rand;
    }
}