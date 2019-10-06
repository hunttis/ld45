using System;
using UnityEngine;

[Serializable]
public class Resource : MonoBehaviour
{
    public virtual string GetResourceType()
    {
        throw new NotImplementedException();
    }
}
