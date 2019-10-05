using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LavaBehavior : MonoBehaviour
{
    private Material _material;

    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time;
        Debug.Log(String.Join(",", _material.GetTexturePropertyNames().AsEnumerable()));
        _material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
        _material.SetTextureOffset("_DetailAlbedoMap", new Vector2(-offset/2, offset/4));


    }
}
