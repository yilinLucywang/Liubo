using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageSystem : MonoBehaviour
{
    
    private GameState gs;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private GameObject panel;

    [SerializeField] private Animator animator; 
    
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        gs = FindObjectOfType<GameState>();
        gs.OnTurnChange.AddListener(ShowTurnChangeMessage);
        gs.OnTurningIntoOwl.AddListener(ShowOwlMessage);
        var score = FindObjectOfType<Score>();
        score.OnScore.AddListener(ShowScoreMessage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void ShowTurnChangeMessage(bool isP1Turn)
    {
        animator.Play("ShowMessage");
        text.text = $"{(isP1Turn ? gs.gameData.playername1 : gs.gameData.playername2)} 's turn!";
    }

    void ShowScoreMessage(ScoreType scoreType, int points)
    {
        animator.Play("ShowMessage");
        text.text = points > 1 ? $"Score {points.ToString()} points!"
                : $"Score {points.ToString()} point!";
    }

    void ShowOwlMessage()
    {
        animator.Play("ShowMessage");
        text.text = "Owl";
    }

    void ShowBlockadeMessage()
    {
        
    }
}
