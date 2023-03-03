using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameState : MonoBehaviour
{
    //Set this to true for the 3D scene, will use Vector3s instead of Vector2s
    [SerializeField] GameObject LiuboBoard;
    public bool Is3DGame = false;

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

    private Vector3 firstTargetPos;

    //prefab to spawn
    public GameObject valid_sign;
    public GameObject canvas;

    private int cur_step = 0;
    private List<GameObject> instantiated_list = new List<GameObject>();



    public List<GameObject> white_pieces = new List<GameObject>();
    public List<GameObject> black_pieces = new List<GameObject>(); 


    private List<Vector3> white_poses = new List<Vector3>();
    private List<Vector3> black_poses = new List<Vector3>();


    public bool TwoDiceSame;
    public int firstOrigPos = -10, firstFinalPos;

    private bool isFirstMoved = false, blockade = false;

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
            //2D version
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            //3D version
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = false;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Clickable>())
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
        firstOrigPos = -10;
    }

    public void RollDice()
    {
        int num_1 = RollSticks();
        int num_2 = RollSticks();
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

    /// <summary>
    /// Random number generator that returns a number between 1-4
    /// Uses the same probabilties as the sticks (3/8 chance of 1, 3/8 chance of 2, 1/8 chance of  3, 1/8 chance of 4)
    /// </summary>
    /// <returns>
    /// roll_result = int between 1 and 4
    /// </returns>
    private int RollSticks()
    {
        int random_value = Random.Range(1, 9);
        //values 1 through 3 == 1 on the sticks
        if (random_value < 4)
            return 1;
        //values 4 through 6 == 2 on the sticks
        else if (random_value < 7)
            return 2;
        //value 7 == 3 on the sticks
        else if (random_value == 7)
            return 3;
        //value 8 == 4 on the sticks
        else
            return 4;


    }

    public void TopClick(){
        if(dice_1 != -1){
            cur_step = dice_1;
            
        }

        if(openLimit == true)
            dice1But.SetActive(false);

        if (is_p1_turn && openLimit == true)
        {
            //2D Version
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = true;
            }
            //3D Version
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = false;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = true;
            }
        }
        else if(!is_p1_turn && openLimit == true)
        {
            //2D Version
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = true;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            //3D Version
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = true;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Clickable>())
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
            //2D Version
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = true;
            }
            //3D Version
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = false;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = true;
            }
        }
        else if(!is_p1_turn && openLimit == true)
        {
            //2D Version
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = true;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            //3D Version
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = true;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Clickable>())
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

    private void ShowPossiblePositions()
    {
        RemoveGreens();
        //piece offbard starting pos: 11,1,30,22
        board bd = gameObject.GetComponent<board>();
        bool is_white = !is_black_chosen;
        if (is_white)
        {
            chosen_piece = chosen_piece - 6;
        }
        List<Vector3> res_list = bd.move(is_white, chosen_piece, cur_step);
        for (int i = 0; i < res_list.Count; i++)
        {
            Vector3 pos = res_list[i];
            if (blockade == true && dice_1 == dice_2)
            {
                if (res_list[i].x - firstTargetPos.x < 0.01f && res_list[i].z - firstTargetPos.z < 0.01f)
                {
                    res_list.RemoveAt(i);
                    for(int j = 0; j<res_list.Count; j++)
                    {
                        Vector3 newPos = res_list[j];
                        SpawnGreen(newPos);
                    }
                }
            }
            else
            {
                SpawnGreen(pos);
            }

        }
    }

    public void MovePiece(Vector3 position)
    {
        blockade = false;
        //call script form score
        Score score = gameObject.GetComponent<Score>();
        
        //TODO: 1. move piece
        if (!is_black_chosen)
        {
            chosen_piece = chosen_piece + 6;
        }

        string chosen_piece_name = chosen_piece.ToString();
        GameObject cur_piece = GameObject.Find(chosen_piece_name);
        board bd = gameObject.GetComponent<board>();
        int pos_index = bd.get_anchor_index(position);


        bool is_two_same_spot = false;

        //check position beofre moving
        int CurOrgIndex = bd.get_anchor_index(cur_piece.transform.position);

        //Moving from off board
        if (CurOrgIndex == -1)
        {
            //Don't know what should go here
        }
        else if (bd.nodes[CurOrgIndex].Count > 1 && GetComponent<board>().isSameColor(CurOrgIndex) == true)
        {
            //form blockade
            blockade = true;
            firstTargetPos = position;
        }


        if (bd.nodes[pos_index].Count >= 1)
        {
            is_two_same_spot = true;
        }
        if (is_two_same_spot)
        {

            if (Is3DGame)
            {
                cur_piece.transform.position = position;
                cur_piece.transform.Translate(new Vector3(0f, 0.14f, 0f));
            }
            else
                cur_piece.transform.position = position + new Vector3(5.0f, 12.5f);

        }
        else
        {
            cur_piece.transform.position = position;
            cur_piece.transform.Translate(new Vector3(0f, 0.04f, 0f));

        }
        //rotate if needed
        Rotate(cur_piece, pos_index);
        //calculate score before add piece 
        //TODO: this is the bool indicating whether the piece should be an owl
        bool is_owl = bd.is_becoming_owl(pos_index);
        score.CalculateScore(position, chosen_piece, is_black_chosen, is_owl);

        bd.nodes[pos_index].Add(chosen_piece);


        if (openLimit == true)
        {
            if (Is3DGame)
            {
                cur_piece.GetComponent<Clickable>().interactable = false;
            }
            else
            {
                cur_piece.GetComponent<Button>().interactable = false;
            }

        }

        if (!is_black_chosen)
        {
            chosen_piece = chosen_piece - 6;
        }
        if (is_black_chosen)
        {
            bd.black_pieces[chosen_piece] = pos_index;
        }
        else
        {
            bd.white_pieces[chosen_piece] = pos_index;
        }

        Debug.Log("count" + bd.nodes[pos_index].Count);
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


    void SpawnGreen(Vector3 spawn_pos){
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

    List<int> horizontalPosition = new List<int>() {
         1,
         3,
         5,
         8,
         11,
         36,
         33,
         17,
         20,
         30,
         24,
         26,
         14,//14 or 28?
         22
      };
    public void Rotate(GameObject cur_piece, int pos_index)
    {
        
            
        if (horizontalPosition.Contains(pos_index))
        {
            //rotate 90 degree: horizontal
            cur_piece.transform.rotation = LiuboBoard.transform.rotation * Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            //rotate back
            cur_piece.transform.rotation = LiuboBoard.transform.rotation * Quaternion.Euler(0f, 90f, 0f);
        }
        
    }
}
