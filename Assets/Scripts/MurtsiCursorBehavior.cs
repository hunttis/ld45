using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurtsiCursorBehavior : MonoBehaviour
{
    private bool _active = true;
    void Update()
    {
        if (!_active)
        {
            return;
        }
        
        var forward = Input.GetAxis("Mouse X");
        var sideways = Input.GetAxis("Mouse Y");

        transform.position += new Vector3(forward, 0f, sideways);

        if (Input.GetButtonDown("Fire1"))
        {
            _active = false;
            GetComponent<PlungerPlacerBehavior>().Activate();
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
