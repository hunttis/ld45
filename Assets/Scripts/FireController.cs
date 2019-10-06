using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireController : MonoBehaviour
{
    public float offset = 0.0f;
    public float reloadTime = 1.0f;
    public float velocity = 10.0f;

    private float _reloadStatus;
    private GameObject _resourceToShoot;
    private const float Tilt = 0.1f;
    private const int NormN = 6; // Quality of the random numbers; the higher the better
    private AudioSource fireSound;

    private void Awake()
    {
        fireSound = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.GetComponent<Resource>()) return;
        _resourceToShoot = other.gameObject;
    }

    private void FixedUpdate()
    {
        if (_reloadStatus < reloadTime) _reloadStatus += 1.0f / 60;
        // ReSharper disable once Unity.PerformanceCriticalCodeInvocation, not called on every frame
        if (_resourceToShoot && _reloadStatus >= reloadTime) Shoot();
    }

    private void Shoot()
    {
        var shooter = transform;
        var shotDirection = shooter.forward;
        _resourceToShoot.transform.position = shooter.position + shotDirection * offset;
        
        var body = _resourceToShoot.GetComponent<Rigidbody>();
        body.angularVelocity = new Vector3(0, 0, 0);
        body.rotation = Quaternion.identity;
        body.velocity = new Vector3(0, 0, 0);
        body.AddForce(shotDirection * velocity, ForceMode.Impulse);

        _reloadStatus -= reloadTime;
        _resourceToShoot = null;
        fireSound.Play(0);

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
