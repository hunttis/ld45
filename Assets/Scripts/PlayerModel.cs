using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    PlayerController _ctrl;
    Transform _t;
    private void Awake()
    {
        _ctrl = FindObjectOfType<PlayerController>();
        if (_ctrl == null) Debug.LogError("PlayerController not found in scene!");
        _t = GetComponent<Transform>();
    }
    private void Start()
    {
        Transform p = transform.parent;
        transform.SetParent(null);
        Destroy(p.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        _t.position = _ctrl.transform.position;
    }
}
