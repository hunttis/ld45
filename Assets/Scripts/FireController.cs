using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireController : MonoBehaviour
{
    public GameObject resource;
    public Vector3 direction; // should be a unit vector
    public float fireInterval;
    public float velocity;

    private bool m_IsLoaded = false;

    private const float Tilt = 0.1f;
    private const int NormN = 6; // Quality of the random numbers; the higher the better

    void Start()
    {
        direction = direction.normalized;
    }

    private void Update()
    {
    }

    void OnCollisionStay(Collision other)
    {
        HandleLoading(other);
    }

    void OnCollisionEnter(Collision other)
    {
        HandleLoading(other);
    }

    private void HandleLoading(Collision other)
    {
        if (m_IsLoaded || !IsLoadable(other)) return;
        m_IsLoaded = true;
        resource = other.gameObject;
        resource.SetActive(false);
        Invoke(nameof(FireResource), fireInterval);
    }

    private bool IsLoadable(Collision other)
    {
        return other.gameObject.CompareTag("Metal");
    }

    void FireResource()
    {
        resource.transform.position = transform.position + Vector3.up + direction;
        resource.SetActive(true);
        var body = resource.GetComponent<Rigidbody>();

        body.AddForce(direction * velocity);

        m_IsLoaded = false;

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