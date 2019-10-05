using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject shotType;
    public float shotSpeed = 1.0f;
    public float reloadTime = 0.5f;
    public float shotOffset = 0.0f;

    private float reloadStatus = 0.0f;
    private bool _isShooting;

    private void Update()
    {
        _isShooting = Input.GetButton("Fire1") && reloadStatus > reloadTime;
    }

    private void FixedUpdate()
    {
        if (reloadStatus < reloadTime) reloadStatus += 1.0f / 60;
        if (_isShooting) Shoot();
    }

    private void Shoot()
    {
        reloadStatus -= reloadTime;
        var shooter = transform;
        var direction = Quaternion.AngleAxis(90, shooter.right) * shooter.forward;
        var shot = Instantiate(shotType, shooter.position + direction * shotOffset, shooter.rotation);
        var shotBody = shot.GetComponent<Rigidbody>();
        if (shotBody) shotBody.AddForce(direction * shotSpeed, ForceMode.Impulse);
    }
}
