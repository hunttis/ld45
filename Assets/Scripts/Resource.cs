using System;
using UnityEngine;

[Serializable]
public class Resource : MonoBehaviour
{
    private AudioSource _bumpSound;

    private void Awake()
    {
        _bumpSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        _bumpSound.Play();
    }

    public virtual string GetResourceType()
    {
        throw new NotImplementedException();
    }
}
