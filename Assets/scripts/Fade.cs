using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public CanvasGroup canvasGroup;

    private bool fadeIn = false;
    private bool fadeOut = false;

    public float timeOfFade;

    private AudioSource bg;
    public float durations;
    public float target_volume;


    private void Start()
    {
        bg = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if(fadeIn == true)
        {
            if(canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += timeOfFade * Time.deltaTime;
                canvasGroup.blocksRaycasts = true;
                if(canvasGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }

        if(fadeOut == true)
        {
            if(canvasGroup.alpha >= 0)
            {
                canvasGroup.alpha -= timeOfFade * Time.deltaTime;
                if (canvasGroup.alpha == 0)
                {
                    fadeOut = false;
                    canvasGroup.blocksRaycasts = false;
                }
            }
        }
    }


    public void FadeIn()
    {
        fadeIn = true;
    }
    public void FadeOut()
    {
        fadeOut = true;
    }


    IEnumerator FadeToGame()
    {
        FadeIn();
        yield return new WaitForSeconds(1.2f);
        SceneManager.LoadScene("Liubo");
    }

    IEnumerator FadeOutToGame(Action OnFadeOut)
    {
        yield return new WaitForSeconds(4f);
        FadeOut();
        yield return new WaitForSeconds(1f);
        OnFadeOut();
    }

    public void GoToGame()
    {
        StartCoroutine(FadeAudio(bg, durations, target_volume));
        StartCoroutine(FadeToGame());
    }

    public void BackToGame(Action OnBackToGame)
    {
        StartCoroutine(FadeOutToGame(OnBackToGame));
    }

    IEnumerator FadeAudio(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
