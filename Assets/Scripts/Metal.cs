using System;
using UnityEngine;

[Serializable]
public class Metal : Resource
{
    public override string GetResourceType()
    {
        return "Metal";
    }
}
