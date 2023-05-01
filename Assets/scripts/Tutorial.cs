using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] slides, buttons;
    private int next = 0, curr = 0, prev = 0;

    private ColorBlock buttonColor;

    private void Awake()
    {
        
    }
    public void showNextSlides()
    {
        Debug.Log("In Show Next Slide");
        if (curr < 11)
        {
            slides[curr].SetActive(false);
            slides[curr + 1].SetActive(true);
            curr++;
        }
        else
        {
            slides[11].SetActive(true);
            curr = 11;
        }

    }

    public void showPrevSlides()
    {
        Debug.Log("In Show Prev Slide");
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
        for (int i = 0; i < 12; i++)
        {
            buttonColor = buttons[i].GetComponent<Button>().colors;
            buttonColor.normalColor = buttonColor.selectedColor;
        }
        switch (clickedName)
        {
            case "Button0":
                prev = curr;
                curr = 0;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);

                buttons[0].GetComponent<Button>().colors = buttonColor;
                for(int i = 1; i < 12; i++)
                {
                    var originColor = buttons[i].GetComponent<Button>().colors;
                    originColor.normalColor = new Color(1, 1, 1, 0);
                    buttons[i].GetComponent<Button>().colors = originColor;
                }
                break;
            case "Button1":
                prev = curr;
                curr = 1;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);

                buttons[1].GetComponent<Button>().colors = buttonColor;
                for (int i = 2; i < 12; i++)
                {
                    var originColor = buttons[i].GetComponent<Button>().colors;
                    originColor.normalColor = new Color(1, 1, 1, 0);
                    buttons[i].GetComponent<Button>().colors = originColor;
                    buttons[0].GetComponent<Button>().colors = originColor;
                }
                break;
            case "Button2":
                prev = curr;
                curr = 2;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);

                buttons[2].GetComponent<Button>().colors = buttonColor;
                for (int i = 3; i < 12; i++)
                {
                    var originColor = buttons[i].GetComponent<Button>().colors;
                    originColor.normalColor = new Color(1, 1, 1, 0);
                    buttons[i].GetComponent<Button>().colors = originColor;
                    buttons[0].GetComponent<Button>().colors = originColor;
                    buttons[1].GetComponent<Button>().colors = originColor;
                }
                break;
            case "Button3":
                prev = curr;
                curr = 3;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);

                buttons[3].GetComponent<Button>().colors = buttonColor;
                for (int i = 4; i < 12; i++)
                {
                    var originColor = buttons[i].GetComponent<Button>().colors;
                    originColor.normalColor = new Color(1, 1, 1, 0);
                    buttons[i].GetComponent<Button>().colors = originColor;
                    for(int j = 0; j < 3; j++)
                    {
                        buttons[j].GetComponent<Button>().colors = originColor;
                    }
                }
                break;
            case "Button4":
                prev = curr;
                curr = 4;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);

                buttons[4].GetComponent<Button>().colors = buttonColor;
                for (int i = 5; i < 12; i++)
                {
                    var originColor = buttons[i].GetComponent<Button>().colors;
                    originColor.normalColor = new Color(1, 1, 1, 0);
                    buttons[i].GetComponent<Button>().colors = originColor;
                    for (int j = 0; j < 4; j++)
                    {
                        buttons[j].GetComponent<Button>().colors = originColor;
                    }
                }
                break;
            case "Button5":
                prev = curr;
                curr = 5;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);

                buttons[5].GetComponent<Button>().colors = buttonColor;
                for (int i = 6; i < 12; i++)
                {
                    var originColor = buttons[i].GetComponent<Button>().colors;
                    originColor.normalColor = new Color(1, 1, 1, 0);
                    buttons[i].GetComponent<Button>().colors = originColor;
                    for (int j = 0; j < 5; j++)
                    {
                        buttons[j].GetComponent<Button>().colors = originColor;
                    }
                }
                break;
            case "Button6":
                prev = curr;
                curr = 6;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);

                buttons[6].GetComponent<Button>().colors = buttonColor;
                for (int i = 7; i < 12; i++)
                {
                    var originColor = buttons[i].GetComponent<Button>().colors;
                    originColor.normalColor = new Color(1, 1, 1, 0);
                    buttons[i].GetComponent<Button>().colors = originColor;
                    for (int j = 0; j < 6; j++)
                    {
                        buttons[j].GetComponent<Button>().colors = originColor;
                    }
                }
                break;
            case "Button7":
                prev = curr;
                curr = 7;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);

                buttons[7].GetComponent<Button>().colors = buttonColor;
                for (int i = 8; i < 12; i++)
                {
                    var originColor = buttons[i].GetComponent<Button>().colors;
                    originColor.normalColor = new Color(1, 1, 1, 0);
                    buttons[i].GetComponent<Button>().colors = originColor;
                    for (int j = 0; j < 7; j++)
                    {
                        buttons[j].GetComponent<Button>().colors = originColor;
                    }
                }
                break;
            case "Button8":
                prev = curr;
                curr = 8;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);

                buttons[8].GetComponent<Button>().colors = buttonColor;
                for (int i = 9; i < 12; i++)
                {
                    var originColor = buttons[i].GetComponent<Button>().colors;
                    originColor.normalColor = new Color(1, 1, 1, 0);
                    buttons[i].GetComponent<Button>().colors = originColor;
                    for (int j = 0; j < 8; j++)
                    {
                        buttons[j].GetComponent<Button>().colors = originColor;
                    }
                }
                break;
            case "Button9":
                prev = curr;
                curr = 9;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);

                buttons[9].GetComponent<Button>().colors = buttonColor;
                for (int i = 10; i < 12; i++)
                {
                    var originColor = buttons[i].GetComponent<Button>().colors;
                    originColor.normalColor = new Color(1, 1, 1, 0);
                    buttons[i].GetComponent<Button>().colors = originColor;
                    for (int j = 0; j < 9; j++)
                    {
                        buttons[j].GetComponent<Button>().colors = originColor;
                    }
                }
                break;
            case "Button10":
                prev = curr;
                curr = 10;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);

                buttons[10].GetComponent<Button>().colors = buttonColor;
                for (int i = 11; i < 12; i++)
                {
                    var originColor = buttons[i].GetComponent<Button>().colors;
                    originColor.normalColor = new Color(1, 1, 1, 0);
                    buttons[i].GetComponent<Button>().colors = originColor;
                    for (int j = 0; j < 10; j++)
                    {
                        buttons[j].GetComponent<Button>().colors = originColor;
                    }
                }
                break;
            case "Button11":
                prev = curr;
                curr = 11;
                slides[prev].SetActive(false);
                slides[curr].SetActive(true);

                buttons[11].GetComponent<Button>().colors = buttonColor;
                for (int i = 0; i < 11; i++)
                {
                    var originColor = buttons[i].GetComponent<Button>().colors;
                    originColor.normalColor = new Color(1, 1, 1, 0);
                    buttons[i].GetComponent<Button>().colors = originColor;
                }

                break;
            default:
                break;

        }
        if (slides[curr].GetComponent<TutorialPage>())
        {
            slides[curr].GetComponent<TutorialPage>().FirstPage();
        }

    }
}
