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
                canvasGroup.blocksRaycasts = false;
                if (canvasGroup.alpha == 0)
                {
                    fadeOut = false;
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
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Liubo");
    }

    IEnumerator FadeOutToGame()
    {
        yield return new WaitForSeconds(2.2f);
        FadeOut();
    }

    public void GoToGame()
    {
        StartCoroutine(FadeToGame());
    }

    public void BackToGame()
    {
        StartCoroutine(FadeOutToGame());
    }
}
