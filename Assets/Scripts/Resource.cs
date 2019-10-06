using System;
using UnityEngine;

[Serializable]
public class Resource : MonoBehaviour
{
    private AudioSource bumpSound;

    private void Awake()
    {
        bumpSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other)
    {
        bumpSound.Play();
    }
    public virtual string GetResourceType()
    {
        throw new NotImplementedException();
    }
}
