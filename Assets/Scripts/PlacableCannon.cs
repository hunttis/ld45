using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacableCannon : MonoBehaviour
{
    Renderer[] _renderers;
    Material _originalMat;
    Material _blueprintMat;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        _blueprintMat = Resources.Load<Material>("PlacementMaterial");
    }

    public void EnableBlueprintMode()
    {
        // Disable particles

        // Set blueprint material

    }
    public void DisableBlueprintMode()
    {

    }
}
