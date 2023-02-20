using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using UnityEngine.EventSystems;
using System; 

public class movePieces : MonoBehaviour
{
    public List<GameObject> anchors = new List<GameObject>();
    public GameObject canvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void choosePiece(){
        GameState bd = canvas.GetComponent<GameState>();
        string subject_string = EventSystem.current.currentSelectedGameObject.name;
        string result_string = Regex.Match(subject_string,@"\d+").Value;
        int index = Int32.Parse(result_string);
        bool is_black = false; 
        if(index <= 5){
            is_black = true;
        }
        board board = canvas.GetComponent<board>();
        if(is_black == board.is_p1_turn){
            throw new Exception("color of the piece is incorrect!");
        }
        else{
            bd.PieceChosen(index,is_black);
        }
    }
}
