using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] slides, buttons;
    private int next = 0, curr = 0;
    public void showNextSlides()
    {
        if (next < 5)
        {
            slides[next].SetActive(false);
            slides[next+1].SetActive(true);
            next++;
        }
        else
        {
            slides[5].SetActive(true);
        }

    }

    public void showPrevSlides()
    {
        if (next > 0)
        {
            slides[next].SetActive(false);
            slides[next - 1].SetActive(true);
            next--;
        }
        else
        {
            next = 0;
            slides[0].SetActive(true);
        }
        
    }
}
