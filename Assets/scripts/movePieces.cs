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
    // private bool dragging = false;
    // private int cnt = 0;
    // private bool put = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if(put){
        //     moveToClosestAnchor();
        // }
        // if(!dragging){
        //     return;
        //     //moveToClosestAnchor();
        // }
        // var mousePosition = GetMousePos();
        // transform.position = mousePosition;
    }


    // public void OnMouseDown(){
    //     cnt = cnt + 1; 
    //     if(cnt%2 == 1){
    //         dragging = true; 
    //         put = false;
    //     }
    //     else{
    //         dragging = false;
    //         put = true;
    //     }

    // }

    // Vector2 GetMousePos(){
    //     return Input.mousePosition;
    // }

    // public void moveToClosestAnchor(){
    //     Vector3 curPos = transform.position; 
    //     int closestIdx = 0; 
    //     float smallest = Vector3.Distance(curPos, anchors[0].transform.position);
    //     for(int i = 0; i < anchors.Count; i++){
    //         float dist = Vector3.Distance(curPos, anchors[i].transform.position);
    //         if(dist < smallest){
    //             closestIdx = i;
    //             smallest = dist;
    //         }
    //     }
    //     transform.position = anchors[closestIdx].transform.position;
    // }


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
