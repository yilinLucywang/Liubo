using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialPage : MonoBehaviour
{
    public Button prevButton;
    public Button nextButton;
    public List<GameObject> lessonPages;
    public TMP_Text pageNumberText;

    private int currentPage;

    // Start is called before the first frame update
    void Start()
    {
        currentPage = 0;
        ActivatePage(currentPage);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PrevPage();
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextPage();
        }
    }

    public void NextPage()
    {
        currentPage++;
        ActivatePage(currentPage);
    }

    public void PrevPage()
    {
        currentPage--;
        ActivatePage(currentPage);
    }

    public void FirstPage()
    {
        currentPage = 0;
        ActivatePage(currentPage);
    }

    private void ActivatePage(int pageNum)
    {
        //Bounds Check page number and activate arrows
        if(pageNum >= lessonPages.Count - 1)
        {
            pageNum = lessonPages.Count - 1;
            prevButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(false);
            //If only one page disable both arrows
            if(pageNum == 0)
            {
                prevButton.gameObject.SetActive(false);
            }
        }
        else if(pageNum <= 0)
        {
            pageNum = 0;
            prevButton.gameObject.SetActive(false);
            nextButton.gameObject.SetActive(true);
        }
        else
        {
            prevButton.gameObject.SetActive(true);
            nextButton.gameObject.SetActive(true);
        }

        //Activate Page
        foreach (GameObject page in lessonPages)
        {
            page.SetActive(false);
        }
        lessonPages[pageNum].SetActive(true);

        //Write page number
        pageNumberText.text = "" + (pageNum+1) + " / " + lessonPages.Count;

        //Set Page Number in code
        currentPage = pageNum;
    }
}
