using System;
using UnityEngine;

public class Corruptable : MonoBehaviour
{
    public float corruptionTolerance = 10.0f;

    private float _corruptionHealth;
    private Vector3 _initScale;

    private void Awake()
    {
        _corruptionHealth = corruptionTolerance;
        _initScale = transform.localScale;
    }

    public void Corrupt(float amount)
    {
        _corruptionHealth -= amount;
        if (_corruptionHealth < corruptionTolerance)
        {
            var scaleVector = new Vector3(_initScale.x, _initScale.y, _initScale.z);
            var scale = 0.2f + 0.8f * _corruptionHealth / corruptionTolerance;
            scaleVector.Scale(new Vector3(scale, scale, scale));
            transform.localScale = scaleVector;
        }

        if (_corruptionHealth < 0) Destroy(gameObject);
    }
}
