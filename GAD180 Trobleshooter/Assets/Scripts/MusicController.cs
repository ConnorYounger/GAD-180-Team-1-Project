using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public float audioFadeMultiplier = 1;
    public float audioVolume = 1;

    private bool fadeIn;
    private bool fadeOut;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.volume = 0;

        audioSource.Play();
        fadeIn = true;
    }

    private void Update()
    {
        FadingIn();
        FadingOut();
    }

    public void FadeIn()
    {
        fadeIn = true;
        fadeOut = false;
    }

    public void FadeOut()
    {
        fadeOut = true;
        fadeIn = false;
    }

    private void FadingIn()
    {
        if (fadeIn && audioSource.volume < audioVolume)
        {
            audioSource.volume += audioFadeMultiplier;
        }
        else
        {
            fadeIn = false;
        }
    }
    private void FadingOut()
    {
        if (fadeOut && audioSource.volume > 0)
        {
            audioSource.volume -= audioFadeMultiplier;
        }
        else if (fadeOut)
        {
            fadeOut = false;
            audioSource.Stop();
        }
    }
}
