using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class StartUI : MonoBehaviour
{

    [SerializeField] private TMP_InputField[] inputField;
    [SerializeField] GameObject startButton, nameButton, tutorial, chineseTutorial, rulesButton, creditButton, ExitButton, credits;
    public GameData data;
    void Start()
    {
        inputField[0].characterLimit = 12;
        inputField[1].characterLimit = 12;
    }
    public void showTutorial()
    {
        if(data.isEN){
            tutorial.SetActive(true);
        }
        else{
            chineseTutorial.SetActive(true);
        }
        startButton.SetActive(false);
        rulesButton.SetActive(false);
        creditButton.SetActive(false);
        ExitButton.SetActive(false);
    }
    public void showName()
    {
        nameButton.SetActive(true);
        rulesButton.SetActive(false);
        startButton.SetActive(false);
        tutorial.SetActive(false);
        chineseTutorial.SetActive(false);
        creditButton.SetActive(false);
        ExitButton.SetActive(false);
    }
    public void StartGame()
    {


        if (inputField[0].text == "")
            data.playername1 = TextProvider.Instance.GetText("text0011"); // "Player1";
        else
            data.playername1 = inputField[0].text;

        if (inputField[1].text == "")
            data.playername2 = TextProvider.Instance.GetText("text0012"); //"Player2";
        else
            data.playername2 = inputField[1].text;


        
    }

    public void Credits()
    {
        credits.SetActive(true);
    }
    public void ExitCredits()
    {
        credits.SetActive(false);
    }


    public void BackToMainPage()
    {
        nameButton.SetActive(false);
        rulesButton.SetActive(true);
        startButton.SetActive(true);
        tutorial.SetActive(false);
        chineseTutorial.SetActive(false);
        creditButton.SetActive(true);
        ExitButton.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Link()
    {
        Application.OpenURL("https://projects.etc.cmu.edu/liubo-lab/credits/");
    }

    public void ChangeLanguage()
    {
        data.isEN ^= true;
    }
}
