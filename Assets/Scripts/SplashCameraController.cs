using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashCameraController : MonoBehaviour
{
    private static readonly int FadeIn = Animator.StringToHash("FadeIn");
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");

    public GameObject ship;
    public GameObject fader;
    public ParticleSystem explosion;

    private Vector3 _shakeOriginalPosition;
    private float _shakeAmount;

    void Start()
    {
        fader.GetComponent<Animator>().SetTrigger(FadeIn);

        _shakeOriginalPosition = transform.localPosition;
        _shakeAmount = 0.5f;

        Invoke(nameof(ShakeHarder), 3.0f);
        Invoke(nameof(StartFade), 5.0f);
        Explode();
    }

    void Update()
    {
        transform.LookAt(ship.transform);
        transform.localPosition = _shakeOriginalPosition + Random.insideUnitSphere * _shakeAmount;
    }

    void ShakeHarder()
    {
        _shakeOriginalPosition = transform.localPosition;
        _shakeAmount = 10.0f;
    }

    void Explode()
    {
        Instantiate(explosion, ship.transform);
    }

    void StartFade()
    {
        fader.GetComponent<Animator>().SetTrigger(FadeOut);
        Invoke(nameof(EndSplash), 3.0f);
    }

    void EndSplash()
    {
        SceneManager.LoadScene("World 00");
    }
}
