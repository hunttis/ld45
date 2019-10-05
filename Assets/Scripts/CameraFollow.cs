using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraFollow : MonoBehaviour
{
    public float turnSpeed = 4.0f;
    public Transform positionFollowTarget;
    public float distance = 5.0f;
    public float angle = 45.0f;
    public float followSpeed = 0.1f;

    private Vector3 _offset;
    private Vector3 _cameraLastTarget;
    private GameObject _player;

    private void Awake()
    {
        _player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        _cameraLastTarget = positionFollowTarget.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, positionFollowTarget.position, followSpeed + Vector3.Distance(transform.position, positionFollowTarget.position)*Time.deltaTime*4.0f);
        transform.LookAt(_player.transform.position + Vector3.up*2);
    }
}