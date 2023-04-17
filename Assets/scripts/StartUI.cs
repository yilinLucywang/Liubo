﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class StartUI : MonoBehaviour
{

    [SerializeField] private TMP_InputField[] inputField;
    [SerializeField] GameObject startButton, nameButton, tutorial, rulesButton, creditButton, ExitButton;
    public GameData data;
    void Start()
    {
        inputField[0].characterLimit = 15;
        inputField[1].characterLimit = 15;
    }
    public void showTutorial()
    {
        tutorial.SetActive(true);
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
        creditButton.SetActive(false);
        ExitButton.SetActive(false);
    }
    public void StartGame()
    {


        if (inputField[0].text == "")
            data.playername1 = "Player1";
        else
            data.playername1 = inputField[0].text;

        if (inputField[1].text == "")
            data.playername2 = "Player2";
        else
            data.playername2 = inputField[1].text;


        SceneManager.LoadScene("Liubo");
    }

    public void BackToMainPage()
    {
        nameButton.SetActive(false);
        rulesButton.SetActive(true);
        startButton.SetActive(true);
        tutorial.SetActive(false);
        creditButton.SetActive(true);
        ExitButton.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
