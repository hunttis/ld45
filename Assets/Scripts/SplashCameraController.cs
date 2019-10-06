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

    void Start()
    {
        fader.GetComponent<Animator>().SetTrigger(FadeIn);
        Invoke(nameof(StartFade), 5.0f);
    }

    void Update()
    {
        transform.LookAt(ship.transform);
    }

    void StartFade()
    {
        fader.GetComponent<Animator>().SetTrigger(FadeOut);
        Invoke(nameof(EndSplash), 3.0f);
    }

    void EndSplash()
    {
        SceneManager.LoadScene("World 01");
    }
}
