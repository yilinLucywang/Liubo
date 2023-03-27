using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameData gameData;
    [SerializeField] private Text player1Score;
    [SerializeField] private Text player2Score;

    void Awake()
    {
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScore() {
        player1Score.text = "Player 1 Score: " + gameData.white_score.ToString();
        player2Score.text = "Player 2 Score: " + gameData.black_score.ToString();

        if (gameData.white_score == 6 || gameData.black_score == 6)
        {
            SceneManager.LoadScene("End");
        }
    }
}
