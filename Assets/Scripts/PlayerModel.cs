using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    PlayerController _ctrl;
    Rigidbody _ctrlRb;
    Transform _t;
    private void Awake()
    {
        _ctrl = FindObjectOfType<PlayerController>();
        if (_ctrl == null) Debug.LogError("PlayerController not found in scene!");
        _ctrlRb = _ctrl.GetComponent<Rigidbody>();
        _t = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        _t.position = _ctrl.transform.position;
        Vector3 lookAtVector = _ctrlRb.velocity.normalized;
        lookAtVector.y = 0;
        _t.LookAt(_t.position + lookAtVector);
    }
}
