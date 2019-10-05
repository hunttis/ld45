using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class LavaBehavior : MonoBehaviour
{
    private Material _material;
    public Vector2 mainTexOffset;
    public Vector2 secondaryTexOffset;

    // Start is called before the first frame update
    void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        float timeOffset = Time.time;
        Debug.Log(String.Join(",", _material.GetTexturePropertyNames().AsEnumerable()));
        _material.SetTextureOffset("_MainTex", mainTexOffset * timeOffset);
        _material.SetTextureOffset("_DetailAlbedoMap", secondaryTexOffset * timeOffset);


    }
}
