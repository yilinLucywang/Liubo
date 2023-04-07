using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class StartUI : MonoBehaviour
{

    [SerializeField] private TMP_InputField[] inputField;
    [SerializeField] GameObject startButton, nameButton;
    public GameData data;
    void Start()
    {
        inputField[0].characterLimit = 6;
        inputField[0].characterLimit = 6;
    }
    public void showName()
    {
        nameButton.SetActive(true);
        startButton.SetActive(false);
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
}
