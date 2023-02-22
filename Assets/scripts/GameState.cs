using System.Collections;
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
    public GameObject rollDicebtn;
    public GameObject whiteTurn, blackTurn;

    public Text num_1_text;
    public Text num_2_text;  

    public bool is_black_chosen = true;
    public int chosen_piece = -1;

    public bool openLimit;


    //prefab to spawn
    public GameObject valid_sign;
    public GameObject canvas;

    private int cur_step = 0;
    private List<GameObject> instantiated_list = new List<GameObject>();



    public List<GameObject> white_pieces = new List<GameObject>();
    public List<GameObject> black_pieces = new List<GameObject>(); 


    private List<Vector2> white_poses = new List<Vector2>();
    private List<Vector2> black_poses = new List<Vector2>();


    void Awake(){
        for(int i = 0; i < white_pieces.Count; i++){
            white_poses.Add(white_pieces[i].transform.position);
            black_poses.Add(black_pieces[i].transform.position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        whiteTurn.SetActive(true);
        if (openLimit == true)
        {
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
        }
        if(openLimit == true)
        {
            dice1But.SetActive(false);
            dice2But.SetActive(false);
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
        
        num_1_text.text = "Dice1: "; 
        num_2_text.text = "Dice2: ";

        rollDicebtn.GetComponent<Button>().interactable = true;

        if (is_p1_turn)
        {
            whiteTurn.SetActive(true);
            blackTurn.SetActive(false);
        }
        if (!is_p1_turn)
        {
            whiteTurn.SetActive(false);
            blackTurn.SetActive(true);
        }

    }

    public void RollDice()
    {
        int num_1 = Random.Range(1, 5);
        int num_2 = Random.Range(1, 5);
        num_1_text.text = "Dice1: " + num_1.ToString(); 
        num_2_text.text = "Dice2: " + num_2.ToString();
        dice_1 = num_1; 
        dice_2 = num_2;

        if(openLimit == true)
        {
            dice1But.SetActive(true);
            dice2But.SetActive(true);

            rollDicebtn.GetComponent<Button>().interactable = false;
        }
    }

    public void TopClick(){
        if(dice_1 != -1){
            cur_step = dice_1;
            
        }

        if(openLimit == true)
            dice1But.SetActive(false);

        if (is_p1_turn && openLimit == true)
        {
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = true;
            }
        }
        else if(!is_p1_turn && openLimit == true)
        {
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = true;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
        }
    }

    public void BottomClick(){
        if(dice_2 != -1){
            cur_step = dice_2;
        }

        if(openLimit == true)
            dice2But.SetActive(false);

        if (is_p1_turn && openLimit == true)
        {
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = true;
            }
        }
        else if(!is_p1_turn && openLimit == true)
        {
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = true;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
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
        //call script form score
        Score score = gameObject.GetComponent<Score>();

        //TODO: 1. move piece
        if (!is_black_chosen){
            chosen_piece = chosen_piece + 6;
        }

        string chosen_piece_name = chosen_piece.ToString();
        GameObject cur_piece = GameObject.Find(chosen_piece_name);
        board bd = gameObject.GetComponent<board>();
        int pos_index = bd.get_anchor_index(position);
        Debug.Log(pos_index);
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

        //calculate score before add piece 
        score.CalculateScore(position, chosen_piece, is_black_chosen);

        bd.nodes[pos_index].Add(chosen_piece);

        if(openLimit == true)
            cur_piece.GetComponent<Button>().interactable = false;

        if(!is_black_chosen){
            chosen_piece = chosen_piece - 6;
        }
        if(is_black_chosen){
            bd.black_pieces[chosen_piece] = pos_index;
        }
        else{
            bd.white_pieces[chosen_piece] = pos_index;
        }
    
    }

    //This index is in range[0, 5]
    public void RemovePiece(int index, bool is_white){
        //This part takes care of the board storage
        board bd = gameObject.GetComponent<board>();
        if(is_white){
            int ori_place = bd.white_pieces[index];
            int board_index = index + 6;
            List<int> cur_pieces = bd.nodes[ori_place]; 
            for(int i = 0; i < cur_pieces.Count; i++){
                if(cur_pieces[i] == board_index){
                    bd.nodes[ori_place].RemoveAt(i);
                    break;
                }
            }
            bd.white_pieces[index] = -1;
            white_pieces[index].transform.position = white_poses[index];
        }
        else{
            int ori_place = bd.black_pieces[index];
            int board_index = index;
            List<int> cur_pieces = bd.nodes[ori_place];
            for(int i = 0; i < cur_pieces.Count; i++){
                if(cur_pieces[i] == board_index){
                    bd.nodes[ori_place].RemoveAt(i);
                    break;
                }
            }
            bd.black_pieces[index] = -1;
            black_pieces[index].transform.position = black_poses[index];
        }
        //This part takes care of the UI part
    }

    public void RemoveTest(){
       // RemovePiece(5, true);
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
