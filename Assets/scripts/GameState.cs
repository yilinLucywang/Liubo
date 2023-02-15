using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    public bool is_p1_turn = true;
    public int dice_1 = -1; 
    public int dice_2 = -1;

    public Text num_1_text;
    public Text num_2_text;  

    public bool is_black_chosen = true;
    public int chosen_piece = -1;


    private int cur_step = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextRound()
    {
        dice_1 = -1; 
        dice_2 = -1; 
        if(is_p1_turn){
            is_p1_turn = false;
        }
        else{
            is_p1_turn = true;
        }

        num_1_text.text = "Dice1: "; 
        num_2_text.text = "Dice2: ";
    }

    public void RollDice()
    {
        int num_1 = Random.Range(1, 5);
        int num_2 = Random.Range(1, 5);
        num_1_text.text = "Dice1: " + num_1.ToString(); 
        num_2_text.text = "Dice2: " + num_2.ToString();
        dice_1 = num_1; 
        dice_2 = num_2;
    }

    public void TopClick(){
        if(dice_1 != -1){
            cur_step = dice_1;
        }
    }

    public void BottomClick(){
        if(dice_2 != -1){
            cur_step = dice_2;
        }
    }

    public void PieceChosen(int index, bool is_black){
        chosen_piece = index; 
        is_black_chosen = is_black;
    }

    private void ShowPossiblePositions(){
        board bd = gameObject.GetComponent<board>();
        bool is_white = !is_black_chosen;
        if(is_white){
            chosen_piece = chosen_piece - 6;
        }
        List<int> res_list = bd.move(is_white,chosen_piece, cur_step);
    }
}
