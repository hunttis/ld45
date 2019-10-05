using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireController : MonoBehaviour
{
    public float offset = 0.0f;
    public float fireInterval = 1.0f;
    public float velocity = 10.0f;


    private bool _isLoaded = false;
    private GameObject _resource;
    private const float Tilt = 0.1f;
    private const int NormN = 6; // Quality of the random numbers; the higher the better

    private void OnTriggerStay(Collider other)
    {
        HandleLoading(other);
    }

    private void HandleLoading(Component other)
    {
        if (_isLoaded || !IsLoadable(other)) return;
        _isLoaded = true;
        _resource = other.gameObject;
        _resource.SetActive(false);
        Invoke(nameof(FireResource), fireInterval);
    }

    private static bool IsLoadable(Component other)
    {
        return other.gameObject.CompareTag("Metal");
    }

    private void FireResource()
    {
        var shooter = transform;
        var shotDirection = shooter.forward;
        _resource.transform.position = shooter.position + shotDirection * offset;
        _resource.SetActive(true);

        var body = _resource.GetComponent<Rigidbody>();
        body.AddForce(shotDirection * velocity, ForceMode.Impulse);

        _isLoaded = false;

//        var theta = NormRand(Tilt);
//        var tau = NormRand(Tilt);
//        direction = Quaternion.Euler(0, theta, 0) * direction;
//
//        var dir2D = Vector3.ProjectOnPlane(direction, Vector3.up);
//
//        direction = Quaternion.AngleAxis(tau,
//                        Vector3.Cross(dir2D, Vector3.up)) * direction;
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
