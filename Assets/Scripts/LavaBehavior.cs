﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class LavaBehavior : MonoBehaviour
{
    public Vector2 mainTexOffset;
    public Vector2 secondaryTexOffset;
    private Material _material;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    private static readonly int DetailAlbedoMap = Shader.PropertyToID("_DetailAlbedoMap");

    // Start is called before the first frame update
    private void Start()
    {
        _material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    private void Update()
    {
        var timeOffset = Time.time;
        _material.SetTextureOffset(MainTex, mainTexOffset * timeOffset);
        _material.SetTextureOffset(DetailAlbedoMap, secondaryTexOffset * timeOffset);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Ground") && other.gameObject.CompareTag("Metal"))
        {
            Destroy(other.gameObject);
        }
    }
}
