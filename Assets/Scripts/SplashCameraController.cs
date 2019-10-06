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

    private AudioSource _audioSource;
    public AudioClip _explosionSoundFx;
    public AudioClip _crashSoundFx;

    void Start()
    {
        fader.GetComponent<Animator>().SetTrigger(FadeIn);

        _shakeOriginalPosition = transform.localPosition;
        _shakeAmount = 0.5f;

        _audioSource = GetComponent<AudioSource>();

        Invoke(nameof(ExplosionSound), 0.5f);
        Invoke(nameof(ExplosionSound), 3.5f);
        Invoke(nameof(CrashSound), 4.7f);
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
        SceneManager.LoadScene("World 00");
    }
}
