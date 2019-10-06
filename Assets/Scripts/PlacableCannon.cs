using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacableCannon : MonoBehaviour
{
    public Transform adjustSeat;

    private Renderer[] _renderers;
    private Material[] _originalMats;

    private FireController _fireController;
    private ResourceMagnet _resourceMagnet;
    private ParticleSystem _particleSystem;

    private Transform _barrel;

    private bool _bluePrintMode = false;
    private bool _highlighted = false;

    private Material _blueprintMat;
    private Material _cantBuildMat;
    private Material _highlightMat;


    private void Awake()
    {
        _barrel = transform.Find("Barrel");
        _renderers = GetComponentsInChildren<Renderer>();
        _originalMats = new Material[_renderers.Length];
        for (int i = 0; i < _renderers.Length; i++)
        {
            _originalMats[i] = _renderers[i].material;
        }

        _blueprintMat = Resources.Load<Material>("PlacementMaterial");
        _cantBuildMat = Resources.Load<Material>("PlacementCantBuildMaterial");
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
            if (!_highlighted) SetMaterials(_highlightMat);
        }
        else
        {
            RestoreMaterials();
        }

        _highlighted = highlightOn;
    }

    public void SetCantBuild(bool cantBuild)
    {
        if (!_bluePrintMode) return;
        SetMaterials(cantBuild ? _cantBuildMat : _blueprintMat);
    }
}
