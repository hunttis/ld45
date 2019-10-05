using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWheelAnimation : MonoBehaviour
{
    PlayerController _ctrl;
    Transform _t;

    private void Awake()
    {
        _t = GetComponent<Transform>();
        _ctrl = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = _ctrl.transform.rotation;
    }
}
