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
        text.text = $"{(isP1Turn ? gs.gameData.playername1 : gs.gameData.playername2)}'s turn!";
    }

    void ShowScoreMessage(ScoreType scoreType, int points)
    {
        animator.Play("ShowMessage");
        var txt = "";
        switch (scoreType)
        {
            case ScoreType.Nest:
                txt += "Reached nest: ";
                break;
            case ScoreType.CaptureNormal:
                txt += "Bird captured: ";
                break;
            case ScoreType.CaptureOwl:
                txt += "Owl captured: ";
                break;
            default:
                break;
        }
        
        text.text = txt + (points > 1 ? $"score {points.ToString()} points!"
                : $"score {points.ToString()} point!");
    }

    void ShowOwlMessage()
    {
        animator.Play("ShowMessage");
        text.text = "Bird upgraded into an Owl!";
    }

    void ShowBlockadeMessage()
    {
        
    }
}
