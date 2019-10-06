using System;
using UnityEngine;

public class CannonCantBuild : MonoBehaviour
{
    public float cantBuildRadius = 5.0f;
    public bool placed;

    private GameObject[] _cannons;
    private PlayerController _player;

    private void Start()
    {
        _cannons = GameObject.FindGameObjectsWithTag("Cannon");
        _player = GameObject.FindGameObjectWithTag("PlayerController").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (placed) return;
        foreach (var cannon in _cannons)
        {
            if (cannon == gameObject) continue;
            var placableCannon = gameObject.GetComponent<PlacableCannon>();
            if (Vector3.Distance(cannon.transform.position, transform.position) < cantBuildRadius)
            {
                placableCannon.SetCantBuild(true);
                _player.cantBuild = true;
                break;
            }
            else
            {
                placableCannon.SetCantBuild(false);
                _player.cantBuild = false;
            }
        }
    }
}
