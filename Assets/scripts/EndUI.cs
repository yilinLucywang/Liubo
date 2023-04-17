﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameData gameData;
    [SerializeField] private Text player1Score;
    [SerializeField] private Text player2Score;
    [SerializeField] private Text Winner;
    [SerializeField] private GameObject WhiteWin, BlackWin;

    void Awake()
    {
    }
    void Start()
    {
        //player1Score.text = "Player 1 Score: " + gameData.white_score.ToString();
        //player2Score.text = "Player 2 Score: " + gameData.black_score.ToString();
        if (player1Score.text != " ")
            player1Score.text = gameData.playername1 + "'s Score: " + gameData.white_score.ToString();
            player2Score.text = gameData.playername2 + "'s Score: " + gameData.black_score.ToString();

        if (gameData.white_score >= 6 )
        {
            Winner.text = gameData.playername1 + " Wins!";
            WhiteWin.SetActive(true);
            BlackWin.SetActive(false);
        }
        else
        {
            Winner.text = gameData.playername2 + " Wins!";
            WhiteWin.SetActive(false);
            BlackWin.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Restart()
    {
        SceneManager.LoadScene("Start");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
