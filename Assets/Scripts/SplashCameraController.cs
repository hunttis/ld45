using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class SplashCameraController : MonoBehaviour
{
    private static readonly int FadeIn = Animator.StringToHash("FadeIn");
    private static readonly int FadeOut = Animator.StringToHash("FadeOut");

    public GameObject ship;
    public GameObject fader;
    public ParticleSystem explosion;

    private Vector3 _shakeOriginalPosition;
    private float _shakeAmount;

    private AudioSource _audioSource;
    public AudioClip _explosionSoundFx;
    public AudioClip _crashSoundFx;
    public float firstExplosionDelay;
    public float secondExplosionDelay;
    public float thirdExplosionDelay;

    public string nextScene;
    public Boolean shouldExplode = true;

    void Start()
    {
        fader.GetComponent<Animator>().SetTrigger(FadeIn);

        _shakeOriginalPosition = transform.localPosition;
        _shakeAmount = 0.5f;

        _audioSource = GetComponent<AudioSource>();

        Invoke(nameof(ExplosionSound), firstExplosionDelay);
        Invoke(nameof(ExplosionSound), secondExplosionDelay);
        Invoke(nameof(CrashSound), thirdExplosionDelay);
        Invoke(nameof(ShakeHarder), 3.0f);
        Invoke(nameof(StartFade), 5.0f);

        if (shouldExplode)
        {
            Explode();
        }
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

    void ExplosionSound()
    {
        _audioSource.PlayOneShot(_explosionSoundFx);
    }

    void CrashSound()
    {
        _audioSource.PlayOneShot(_crashSoundFx);
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
        SceneManager.LoadScene(nextScene);
    }
}
