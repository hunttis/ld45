using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public Transform player;
    public float distance = 5.0f;
    public float angle = 45.0f;
    public float followSpeed = 0.1f;

    private Vector3 _offset;
    private Vector3 _cameraLastTarget;

    private void Start()
    {
        _cameraLastTarget = player.position;
    }

    private void FixedUpdate()
    {
        _cameraLastTarget = Vector3.Lerp(_cameraLastTarget, player.position, followSpeed * 2);
        var angleOfAttack = Quaternion.AngleAxis(angle, Vector3.right);
        _offset = angleOfAttack * -player.forward * distance;
         transform.position = Vector3.Lerp(_cameraLastTarget, _cameraLastTarget + _offset, followSpeed);
         transform.LookAt(_cameraLastTarget);
    }
}