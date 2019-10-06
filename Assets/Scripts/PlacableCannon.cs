using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacableCannon : MonoBehaviour
{
    Renderer[] _renderers;
    Material[] _originalMats;
    Material _blueprintMat;
    Material _highlightMat;

    FireController _fireController;
    ResourceMagnet _resourceMagnet;
    ParticleSystem _particleSystem;

    public Transform AdjustSeat;

    private Transform _barrel;

    bool _bluePrintMode = false;
    bool _highlighted = false;

    private void Awake()
    {
        _barrel = transform.Find("Barrel");
        _renderers = GetComponentsInChildren<Renderer>();
        _originalMats = new Material[_renderers.Length];
        _blueprintMat = Resources.Load<Material>("PlacementMaterial");
        _highlightMat = Resources.Load<Material>("HighlightMaterial");
        _fireController = GetComponent<FireController>();
        _resourceMagnet = GetComponentInChildren<ResourceMagnet>();
        _particleSystem = GetComponentInChildren<ParticleSystem>();
        GetComponent<AudioSource>().Play();
    }

    private void BlueprintMode(bool setOn)
    {
        _bluePrintMode = setOn;

        // Disable fire control and magnet
        _fireController.enabled = !setOn;
        _resourceMagnet.enabled = !setOn;

        // Disable particles
        
        if (setOn) _particleSystem.Pause(true);
        else _particleSystem.Play(true);

        // Set blueprint material to renderers

        if (setOn)
        {
            SetMaterials(_blueprintMat);
        }
        else
        {
            RestoreMaterials();
        }
    }

    private void SetMaterials(Material to)
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _originalMats[i] = _renderers[i].material;
            _renderers[i].material = to;
        }
    }

    private void RestoreMaterials()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material = _originalMats[i];
        }
    }

    public void EnableBlueprintMode()
    {
        BlueprintMode(true);
    }
    public void DisableBlueprintMode()
    {
        BlueprintMode(false);
    }
    public void SetAngle(float to)
    {
        _fireController.transform.localRotation = Quaternion.Euler(to, 0, 0);
    }
    
    public void AddAngle(float by)
    {
        _barrel.Rotate(Vector3.up, by, Space.Self);
    }

    public void SetRotation(float to)
    {
        _fireController.transform.localRotation = Quaternion.Euler(0, to, 0);
    }
    
    public void AddRotation(float by)
    {
        transform.Rotate(Vector3.up, by, Space.World);
    }
    
    public void SetHighlighted(bool highlightOn)
    {
        if (_bluePrintMode) return;
        if (highlightOn)
        {
            if(!_highlighted) SetMaterials(_highlightMat);
        }
        else
        {
            RestoreMaterials();
        }

        _highlighted = highlightOn;
    }
}
