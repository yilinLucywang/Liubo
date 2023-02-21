﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public bool is_p1_turn = true;
    public int dice_1 = -1; 
    public int dice_2 = -1;

    public GameObject dice1But, dice2But;
    public GameObject blackPieceBut, whitePieceBut;

    public Text num_1_text;
    public Text num_2_text;  

    public bool is_black_chosen = true;
    public int chosen_piece = -1;


    //prefab to spawn
    public GameObject valid_sign;
    public GameObject canvas;

    private int cur_step = 0;
    private List<GameObject> instantiated_list = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var btn in blackPieceBut.GetComponentsInChildren<Button>())
        {
            btn.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextRound()
    {
        is_p1_turn = !is_p1_turn;
        dice_1 = -1; 
        dice_2 = -1; 
        if(is_p1_turn){
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = true;
            }
        }
        else{
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = true;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
        }
        dice1But.SetActive(true);
        dice2But.SetActive(true);
        num_1_text.text = "Dice1: "; 
        num_2_text.text = "Dice2: ";
    }

    public void RollDice()
    {
        Debug.Log("hihihih");
        int num_1 = Random.Range(1, 5);
        int num_2 = Random.Range(1, 5);
        Debug.Log(num_1_text.text);
        Debug.Log("22222");
        num_1_text.text = "Dice1: " + num_1.ToString(); 
        num_2_text.text = "Dice2: " + num_2.ToString();
        dice_1 = num_1; 
        dice_2 = num_2;
    }

    public void TopClick(){
        if(dice_1 != -1){
            cur_step = dice_1;
            dice1But.SetActive(false);
        }
    }

    public void BottomClick(){
        if(dice_2 != -1){
            cur_step = dice_2;
            dice2But.SetActive(false);
        }
    }

    public void PieceChosen(int index, bool is_black){
        chosen_piece = index; 
        is_black_chosen = is_black;
        //show all possibe positions
        ShowPossiblePositions();
    }

    private void ShowPossiblePositions(){
        //piece offbard starting pos: 11,1,30,22
        board bd = gameObject.GetComponent<board>();
        bool is_white = !is_black_chosen;
        if(is_white){
            chosen_piece = chosen_piece - 6;
        }
        List<Vector2> res_list = bd.move(is_white,chosen_piece, cur_step);
        for(int i = 0; i < res_list.Count; i++){
            Vector2 pos = res_list[i];
            SpawnGreen(pos);
        }
    }

    public void MovePiece(Vector2 position){
        //TODO: 1. move piece
        if(!is_black_chosen){
            chosen_piece = chosen_piece + 6;
        }

        string chosen_piece_name = chosen_piece.ToString();
        GameObject cur_piece = GameObject.Find(chosen_piece_name);
        board bd = gameObject.GetComponent<board>();
        int pos_index = bd.get_anchor_index(position);
        bool is_two_same_spot = false; 
        if(bd.nodes[pos_index].Count >= 1){
            is_two_same_spot = true;
        }

        if(is_two_same_spot){
            cur_piece.transform.position = position + new Vector2(5.0f,12.5f); 
        }
        else{
            cur_piece.transform.position = position;
        }

        bd.nodes[pos_index].Add(chosen_piece);

        cur_piece.GetComponent<Button>().interactable = false;

        if(!is_black_chosen){
            chosen_piece = chosen_piece - 6;
        }
        Debug.Log(pos_index);
        Debug.Log("num name: "); 
        Debug.Log(chosen_piece);
        if(is_black_chosen){
            bd.black_pieces[chosen_piece] = pos_index;
        }
        else{
            bd.white_pieces[chosen_piece] = pos_index;
        }
    
    }


    void SpawnGreen(Vector2 spawn_pos){
        //spawn the prefab at the given position
        GameObject instantiated = Instantiate(valid_sign, spawn_pos, Quaternion.identity);
        instantiated.transform.SetParent(canvas.transform);
        instantiated_list.Add(instantiated);
    }

    public void RemoveGreens(){
        for(int i = 0; i < instantiated_list.Count; i++){
            Destroy(instantiated_list[i]);
        }
    }


    public void SwitchToNextRound(){
        board bd = gameObject.GetComponent<board>();
        //TODO: switch the color of the piece
        if(bd.is_p1_turn){
            bd.is_p1_turn = false;
        }
        else{
            bd.is_p1_turn = true;
        }
        //TODO: clear up the number on the board and also in the board.cs
        bd.cur_rolls[0] = 0;
        bd.cur_rolls[1] = 0;

        dice_1 = -1; 
        dice_2 = -1;
        num_1_text.text = "Dice1: "; 
        num_2_text.text = "Dice2: ";   
    }
}
