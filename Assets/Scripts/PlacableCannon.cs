using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacableCannon : MonoBehaviour
{
    Renderer[] _renderers;
    Material[] _originalMats;
    Material _blueprintMat;

    FireController[] _fireControllers;
    ResourceMagnet[] _resourceMagnets;
    ParticleSystem[] _particles;
    
    FireController _fc;

    public Transform AdjustSeat;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        _originalMats = new Material[_renderers.Length];
        _blueprintMat = Resources.Load<Material>("PlacementMaterial");
        _fireControllers = GetComponentsInChildren<FireController>();
        _resourceMagnets = GetComponentsInChildren<ResourceMagnet>();
        _particles = GetComponentsInChildren<ParticleSystem>();
        _fc = GetComponentInChildren<FireController>();
    }

    private void BlueprintMode(bool setOn)
    {
        // Disable fire control and magnet
        foreach (FireController fctrl in _fireControllers) fctrl.enabled = !setOn;
        foreach (ResourceMagnet rmgnt in _resourceMagnets) rmgnt.enabled = !setOn;

        // Disable particles

        foreach (ParticleSystem particle in _particles)
        {
            if (setOn) particle.Pause(true);
            else particle.Play(true);
        }

        // Set blueprint material to renderers

        for (int i = 0; i < _renderers.Length; i++)
        {
            if (setOn)
            {
                _originalMats[i] = _renderers[i].material;
                _renderers[i].material = _blueprintMat;
            } else
            {
                _renderers[i].material = _originalMats[i];
            }
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
        _fc.transform.localRotation = Quaternion.Euler(to, 0, 0);
    }
    public void AddAngle(float by)
    {
        _fc.transform.Rotate(Vector3.left, by, Space.Self);
    }

    public void SetRotation(float to)
    {
        _fc.transform.localRotation = Quaternion.Euler(0, to, 0);

    }
    public void AddRotation(float by)
    {
        _fc.transform.Rotate(Vector3.up, by, Space.World);

    }
}
