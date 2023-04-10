using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] slides, buttons;
    private int next = 0, curr = 0, prev = 0;
    public void showNextSlides()
    {
        if (curr < 5)
        {
            slides[curr].SetActive(false);
            slides[curr + 1].SetActive(true);
            curr++;
        }
        else
        {
            slides[5].SetActive(true);
            curr = 5;
        }

    }

    public void showPrevSlides()
    {
        if (curr > 0)
        {
            slides[curr].SetActive(false);
            slides[curr - 1].SetActive(true);
            curr--;
        }
        else
        {
            curr = 0;
            slides[0].SetActive(true);
        }
        
    }

    public void showSlide()
    {
        string clickedName = EventSystem.current.currentSelectedGameObject.name;
        switch (clickedName)
        {
            case "Button0":
                prev = curr;
                curr = 0;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);
                break;
            case "Button1":
                prev = curr;
                curr = 1;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);
                break;
            case "Button2":
                prev = curr;
                curr = 2;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);
                break;
            case "Button3":
                prev = curr;
                curr = 3;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);
                break;
            case "Button4":
                prev = curr;
                curr = 4;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);
                break;
            case "Button5":
                prev = curr;
                curr = 5;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);
                break;
            default:
                break;

        }

    }
}
