﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Playables;
using Random = UnityEngine.Random;

public class GameState : MonoBehaviour
{
    //Set this to true for the 3D scene, will use Vector3s instead of Vector2s
    [SerializeField] GameObject LiuboBoard;
    public AudioSource ac;
    public bool Is3DGame = false;

    public bool is_p1_turn = false;
    public int dice_1 = -1; 
    public int dice_2 = -1;

    public GameObject dice1But, dice2But, dice1But2, dice2But2;
    public GameObject blackPieceBut, whitePieceBut;
    public GameObject rollDicebtn;
    public GameObject whiteTurn, blackTurn;
    public GameData gameData;
    public GameObject boardCharacter;
    public GameObject stickJar, throwingSticks;
    public Material whiteMat, blackMat;

    public GameObject WhiteNestHighlights;
    public GameObject BlackNestHighlights;

    public Text num_1_text, num_2_text, num_1_text2, num_2_text2;  

    public bool is_black_chosen = true;
    public int chosen_piece = -1;

    public bool topClicked = false;
    public bool bottomClicked = false;


    public bool openLimit;

    private Vector3 firstTargetPos;
    private string previousPiece;
    private bool isSamePiece = false;
    public bool notFirstRound = false;
    private GameObject cur_piece;
    public bool isCharacterOn;

    //prefab to spawn
    public GameObject valid_sign;
    public GameObject canvas;

    //stop sign to spawn
    public GameObject stop_sign; 
    private List<GameObject> instantiated_stop_list = new List<GameObject>();

    public int cur_step = 0;
    private List<GameObject> instantiated_list = new List<GameObject>();

    public List<GameObject> white_pieces = new List<GameObject>();
    public List<GameObject> black_pieces = new List<GameObject>(); 


    private List<Vector3> white_poses = new List<Vector3>();
    private List<Vector3> black_poses = new List<Vector3>();


    public bool TwoDiceSame;
    public int firstOrigPos = -10, firstFinalPos;

    public bool isFirstMoved = false, startedFromABlockade = false;
    public int FormerBlockadeCompany = -1;
    
    public DestinationMouseEvent OnDestinationMouseEnterEvent = new DestinationMouseEvent();
    public UnityEvent OnDestinationMouseExitEvent = new UnityEvent();
    public UnityEvent OnPieceLand = new UnityEvent();
    public UnityEvent OnPieceStartMoving = new UnityEvent();
    // When player press ESC to deselect both the piece and the move
    public UnityEvent OnDeselect = new UnityEvent();
    public TurnChangeEvent OnTurnChange = new TurnChangeEvent(); // is P1 turn or not
    public UnityEvent OnBlockadeForm = new UnityEvent();
    public UnityEvent OnTurningIntoOwl = new UnityEvent();
    
    private board bd;
    private int pieceMoved = 0;

    private Coroutine pieceMovingCoroutine;
    public State state = State.Roll;

    public Transform[] allwhitePieces;
    public Transform[] allblackPieces;

    public PlayableDirector whiteSideTimeLine;
    public PlayableDirector blackSideTimeLine;

    public Fade fadeInOut;
    public GameObject fadeInImg;
    
    void Awake(){
        for(int i = 0; i < white_pieces.Count; i++){
            white_poses.Add(white_pieces[i].transform.position);
            black_poses.Add(black_pieces[i].transform.position);
        }

        fadeInOut = FindObjectOfType<Fade>();
        fadeInImg.SetActive(true);
        fadeInOut.BackToGame(NextRound);
    }

    // Start is called before the first frame update
    void Start()
    {
        state = State.Roll;

        whiteSideTimeLine.Stop();
        

        allwhitePieces = whitePieceBut.GetComponentsInChildren<Transform>();
        allblackPieces = blackPieceBut.GetComponentsInChildren<Transform>();

        isCharacterOn = true;
        if(is_p1_turn == true)
        {
            whiteTurn.GetComponentInChildren<Text>().text = gameData.playername1 + "'s Turn";
            // whiteTurn.SetActive(true);
        }
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
            dice1But2.SetActive(false);
            dice2But2.SetActive(false);
        }
        bd = GetComponent<board>();
        StickRoller.GetInstance().SetActive(false);
        // NextRound();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonUp(1)) && (state & State.MoveOrPieceSelection) != 0)
        {
            DeselectButtonAndPiece();
        }

        if(state == State.MoveOrPieceSelection && is_p1_turn)
        {
            
            whiteSideTimeLine.time = 0;
            whiteSideTimeLine.Play();
        }

        if(state == State.MoveOrPieceSelection && !is_p1_turn)
        {
            blackSideTimeLine.time = 0;
            blackSideTimeLine.Play();
        }

        
    }

    public void ShowBoardCharacter()
    {
        if(isCharacterOn == false)
        {
            boardCharacter.SetActive(true);
            isCharacterOn = true;
        } else if(isCharacterOn == true)
        {
            boardCharacter.SetActive(false);
            isCharacterOn = false;
        }
        
    }

    public void NextRound()
    {
        state = State.Roll;
        is_p1_turn = !is_p1_turn;
        dice_1 = -1; 
        dice_2 = -1;

        StickRoller.GetInstance().SetActive(true);
        throwingSticks.SetActive(true);
        
        num_1_text.text = ""; 
        num_2_text.text = "";

        num_1_text2.text = "";
        num_2_text2.text = "";

        rollDicebtn.GetComponent<Button>().interactable = true;
        
        foreach(Transform child in allwhitePieces)
        {
            child.gameObject.layer = 0;
            child.gameObject.GetComponentInChildren<MeshRenderer>().material = whiteMat;
        }

        foreach(Transform child in allblackPieces)
        {
            child.gameObject.layer = 0;
            child.gameObject.GetComponentInChildren<MeshRenderer>().material = blackMat;
        }

        if (is_p1_turn)
        {
            blackTurn.SetActive(false);
            whiteTurn.GetComponentInChildren<Text>().text = gameData.playername1 + "'s Turn";
            // whiteTurn.SetActive(true);
            WhiteNestHighlights.SetActive(true);
            BlackNestHighlights.SetActive(false);
            
        }
        if (!is_p1_turn)
        {
            whiteTurn.SetActive(false);
            blackTurn.GetComponentInChildren<Text>().text = gameData.playername2 + "'s Turn";
            // blackTurn.SetActive(true);
            WhiteNestHighlights.SetActive(false);
            BlackNestHighlights.SetActive(true);

        }
        OnTurnChange.Invoke(is_p1_turn);
        firstOrigPos = -10;
    }

    private void SetUpForMove()
    {
        DeselectButtonAndPiece();
        // enable clickable pieces
        if (is_p1_turn && openLimit == true)
        {
            //3D Version
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = false;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = true;
            }
            if (notFirstRound == true && previousPiece == cur_piece.ToString())
            {
                cur_piece.GetComponent<Clickable>().interactable = false;
                cur_piece.layer = 2;
            }
        }
        else if(!is_p1_turn && openLimit == true)
        {
            //3D Version
            foreach (var btn in blackPieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = true;
            }
            foreach (var btn in whitePieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = false;
            }
            if (notFirstRound == true && previousPiece == cur_piece.ToString())
            {
                cur_piece.GetComponent<Clickable>().interactable = false;
                cur_piece.layer = 2;
            }
        }
    }
    
    public void RollDice()
    {
        // int num_1 = RollSticks();
        // int num_2 = RollSticks();
        var (num_1, num_2) = StickRoller.GetInstance().RollSticks();
        num_1_text.text = num_1.ToString(); 
        num_2_text.text = num_2.ToString();
        num_1_text2.text = num_1.ToString();
        num_2_text2.text = num_2.ToString();


        dice_1 = num_1; 
        dice_2 = num_2;


        if (openLimit == true)
        {
            StopCoroutine(WaitForClickBtn());
            StartCoroutine(WaitForClickBtn());


            rollDicebtn.GetComponent<Button>().interactable = false;
        }

    }

    IEnumerator WaitForClickBtn()
    {
        yield return new WaitForSeconds(1.71f);
        state = State.MoveOrPieceSelection;
        StickRoller.GetInstance().SetActive(false);
        throwingSticks.SetActive(false);
        if (is_p1_turn)
        {
            dice1But.SetActive(true);
            dice2But.SetActive(true);
            dice1But.GetComponent<Button>().interactable = true;
            dice2But.GetComponent<Button>().interactable = true;
        }
        else if (!is_p1_turn)
        {
            dice1But2.SetActive(true);
            dice2But2.SetActive(true);
            dice1But2.GetComponent<Button>().interactable = true;
            dice2But2.GetComponent<Button>().interactable = true;
        }
        SetUpForMove();
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

    public void TopClick()
    {
        if ((state & State.MoveOrPieceSelection) == 0) return;
        if(dice_1 != -1){
            cur_step = dice_1;
            
        }
        
        RemoveGreens();
        topClicked = true;
        bottomClicked = false;
        state |= State.MoveSelected;

        if (is_p1_turn && openLimit == true)
        {
            dice1But.GetComponent<Button>().interactable = false;
            dice2But.GetComponent<Button>().interactable = true;

            whiteSideTimeLine.time = 0;
            whiteSideTimeLine.Stop();
            whiteSideTimeLine.Evaluate();
        }
        else if(!is_p1_turn && openLimit == true)
        {
            dice1But2.GetComponent<Button>().interactable = false;
            dice2But2.GetComponent<Button>().interactable = true;

            blackSideTimeLine.time = 0;
            blackSideTimeLine.Stop();
            blackSideTimeLine.Evaluate();
        }
        
        if (state == (State.MoveOrPieceSelection | State.MoveSelected | State.PieceSelected))
        { 
            ShowPossiblePositions();
        }
    }

    public void BottomClick(){
        if ((state & State.MoveOrPieceSelection) == 0) return;

        if(dice_2 != -1){
            cur_step = dice_2;
        }
        
        RemoveGreens();
        topClicked = false;
        bottomClicked = true;
        state |= State.MoveSelected;


        if (is_p1_turn && openLimit == true)
        {
            dice1But.GetComponent<Button>().interactable = true;
            dice2But.GetComponent<Button>().interactable = false;
            whiteSideTimeLine.time = 0;
            whiteSideTimeLine.Stop();
            whiteSideTimeLine.Evaluate();
        }
        else if(!is_p1_turn && openLimit == true)
        {
            dice1But2.GetComponent<Button>().interactable = true;
            dice2But2.GetComponent<Button>().interactable = false;
            blackSideTimeLine.time = 0;
            blackSideTimeLine.Stop();
            blackSideTimeLine.Evaluate();
        }
        
        if (state == (State.MoveOrPieceSelection | State.MoveSelected | State.PieceSelected))
        {
            ShowPossiblePositions();
        }
    }


    public void PieceChosen(int index, bool is_black)
    {
        //blockade = false;
        if ((state & State.MoveOrPieceSelection) == 0) return;
        state |= State.PieceSelected;
        chosen_piece = index; 
        is_black_chosen = is_black;
        cur_piece = GameObject.Find(chosen_piece.ToString());
        //show all possibe positions
        Debug.Log("here! line 352");
        if (state == (State.MoveOrPieceSelection | State.MoveSelected | State.PieceSelected))
        {
            ShowPossiblePositions();
            
        }
    }

    private void ShowPossiblePositions()
    {
        RemoveGreens();
        //piece offbard starting pos: 11,1,30,22
        board bd = gameObject.GetComponent<board>();
        bool is_white = !is_black_chosen;
        List<Vector3> res_list = bd.move(is_white, is_white ? chosen_piece - 6 : chosen_piece, cur_step);
        Debug.Log("line 369");
        Debug.Log(res_list.Count);

        for(int k = 0; k < bd.final_paths.Count; k++){
            string pathString = "";
            for(int j = 0; j < bd.final_paths[k].Count; j++){
                pathString += bd.final_paths[k][j].ToString();
            }
        }

        res_list.RemoveAll(destNodePos =>
        {
            var nodeIndex = bd.get_anchor_index(destNodePos);
            return startedFromABlockade == true 
                   && chosen_piece == FormerBlockadeCompany 
                   && nodeIndex == bd.get_anchor_index(firstTargetPos);
        });

        for (int i = 0; i < res_list.Count; i++)
        {
            Vector3 pos = res_list[i];
            var nodeIndex = bd.get_anchor_index(res_list[i]);
            var rotation = GetPieceOrientation(nodeIndex, false);
            SpawnGreen(pos, rotation);
        }
    }

    private void DeselectButtonAndPiece()
    {
        state = State.MoveOrPieceSelection;
        chosen_piece = -1;
        cur_step = 0;
        RemoveGreens();
        if (is_p1_turn)
        {
            dice1But.GetComponent<Button>().interactable = true;
            dice2But.GetComponent<Button>().interactable = true;
        }
        else if (!is_p1_turn)
        {
            dice1But2.GetComponent<Button>().interactable = true;
            dice2But2.GetComponent<Button>().interactable = true;
        }
        OnDeselect.Invoke();
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
    public void piecePlacement(int pos_index, Vector3 anchor_pos){
        //Debug.Log("388");
        board bd = gameObject.GetComponent<board>();
        bool is_two_same_spot = false;
        if (bd.nodes[pos_index].Count >= 1 && (bd.nodes[pos_index][0] != chosen_piece))
        {
            is_two_same_spot = true;
        }
        bool isSecondOne = false;
        var areDiff = hasDifferentType(pos_index);
        for(int i = 0; i < bd.nodes[pos_index].Count; i++){
            int chosen_piece_index = bd.nodes[pos_index][i];
            string chosen_piece_name = chosen_piece_index.ToString();
            GameObject cur_piece = GameObject.Find(chosen_piece_name);
            bool isOwl = cur_piece.CompareTag("Owl");
            piecePlacement(pos_index, isOwl, isSecondOne, cur_piece, anchor_pos, areDiff);
            isSecondOne = true;
        }
    }

    public void piecePlacement(int pos_index, bool isOwl, bool isSecondOne, GameObject cur_piece, Vector3 anchor_pos, bool isDiff){
        ////Debug.Log("408");
        Quaternion curPieceRotation = Quaternion.Euler(0f, 0f, 0f);
        Vector3 curPieceTranslation = new Vector3(0f,0f,0f);
        bool isHorizontal = false;
        if (horizontalPosition.Contains(pos_index))
        {
            //rotate 90 degree: horizontal
            isHorizontal = true;
            curPieceRotation = LiuboBoard.transform.rotation * Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            //rotate back
            curPieceRotation = LiuboBoard.transform.rotation * Quaternion.Euler(0f, 90f, 0f);
        }
        if(isOwl){
            // single owl
            curPieceRotation = curPieceRotation * Quaternion.Euler(0f, 90f, 90f);
            curPieceTranslation = new Vector3(0f, bd.pieceHeight / 2, 0f);
            
            // 1 owl 1 normal, owl on the top
            if (isDiff)
            {
                curPieceTranslation += new Vector3(0f, bd.pieceWidth, 0f);
            }
            // 2 owl, the second owl
            else if(isSecondOne){
                if(isHorizontal){
                    curPieceTranslation += new Vector3(0f,0f,0.15f);
                }
                else{
                    curPieceTranslation += new Vector3(0.15f,0f,0f);
                }
            }
        }
        else{
            curPieceRotation = curPieceRotation * Quaternion.Euler(0f, 0f, 0f);
            // top normal of 2 normals
            if (isSecondOne && !isDiff){
                curPieceTranslation = new Vector3(0f, bd.pieceWidth * 1.5f, 0f);
            }
            // single normal, or the bottom normal of 2 pieces 
            else{
                curPieceTranslation = new Vector3(0f, bd.pieceWidth / 2, 0f);
            }
        }

        cur_piece.transform.position = anchor_pos;
        //cur_piece.transform.Translate(curPieceTranslation);
        cur_piece.transform.position = cur_piece.transform.position + curPieceTranslation;
        cur_piece.transform.rotation = curPieceRotation;
    }


    public bool hasDifferentType(int pos_index){
        board bd = gameObject.GetComponent<board>();
        if(bd.nodes[pos_index].Count == 2){
            var p1 = GameObject.Find(bd.nodes[pos_index][0].ToString());
            var p2 = GameObject.Find(bd.nodes[pos_index][1].ToString());
            return p1.CompareTag("Owl") != p2.CompareTag("Owl");
        }
        return false;
    }

    public void MovePiece(Vector3 position)
    {

        state = State.PieceMoving;
        OnPieceStartMoving.Invoke();
        StartCoroutine(MovePieceCoroutine(position));

        if(is_p1_turn && topClicked == true)
        {
            dice1But.SetActive(false);
        } else if(is_p1_turn && bottomClicked == true)
        {
            dice2But.SetActive(false);
        } else if(!is_p1_turn && topClicked == true)
        {
            dice1But2.SetActive(false);
        } else if(!is_p1_turn && bottomClicked == true)
        {
            dice2But2.SetActive(false);
        }
    }

    private IEnumerator MovePieceCoroutine(Vector3 position)
    {
        notFirstRound = true;
        startedFromABlockade = false;
        //call script form score
        Score score = gameObject.GetComponent<Score>();
        
        //TODO: 1. move piece
        bool wasOwl = (!cur_piece.CompareTag("Normal"));

        board bd = gameObject.GetComponent<board>();
        int pos_index = bd.get_anchor_index(position);
        var path = bd.final_paths.First(p => p[p.Count - 1] == pos_index);
        if ((chosen_piece >= 6 && bd.white_pieces[chosen_piece - 6] != -1) || (chosen_piece < 6 && bd.black_pieces[chosen_piece] != -1))
        {
            path.RemoveAt(0);
        }
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
            startedFromABlockade = true;
            FormerBlockadeCompany = bd.nodes[CurOrgIndex].First(pieceIndex => pieceIndex != chosen_piece);
            firstTargetPos = position;
            //Debug.Log("first pos" + bd.get_anchor_index(firstTargetPos));
        }


        if (bd.nodes[pos_index].Count >= 1 && (bd.nodes[pos_index][0] != chosen_piece))
        {
            is_two_same_spot = true;
        }
        bool is_owl = willBeOwl(path, cur_piece.CompareTag("Owl"));


        //TODO: check whether the other piece is of the same type
        // bool isDiff = isDifferentType(pos_index, cur_piece.CompareTag("Owl"), chosen_piece);
        // if(is_owl || cur_piece.CompareTag("Owl")){
        //     piecePlacement(pos_index, true, is_two_same_spot, cur_piece, position, isDiff);
        // }
        // else{
        //     piecePlacement(pos_index, false, is_two_same_spot, cur_piece, position, isDiff);
        // }
        //update board, remove piece from the original position, add it to the new position
        for(int i = 0; i < bd.nodes[CurOrgIndex].Count; i++){
            //piece index 0 - 11
            if(bd.nodes[CurOrgIndex][i] == chosen_piece){
                bd.nodes[CurOrgIndex].RemoveAt(i);
                piecePlacement(CurOrgIndex, new Vector3(cur_piece.transform.position.x, 0.04f, cur_piece.transform.position.z));
                break;
            }
        }

        // Perform moving animation, delay all the following until the animation completes
        yield return StartCoroutine(MovePieceAnimation(cur_piece, path));
        
        bd.nodes[pos_index].Add(chosen_piece);


        //calculate score before add piece 
        //TODO: this is the bool indicating whether the piece should be an owl
        score.CalculateScore(pos_index, chosen_piece, cur_piece, is_black_chosen, is_owl);
        
        piecePlacement(pos_index, position);
        
        if (openLimit == true)
        {
            previousPiece = cur_piece.ToString();
            if (Is3DGame)
            {
                //previousPiece = cur_piece;
            }

        }
        
        if (is_black_chosen)
        {
            bd.black_pieces[chosen_piece] = pos_index;
        }
        else
        {
            bd.white_pieces[chosen_piece - 6] = pos_index;
        }

        if (openLimit == true)
        {
            
        }

        ////Debug.Log("count" + bd.nodes[pos_index].Count);
        OnPieceLand.Invoke();

        yield return new WaitForSeconds(1f);
        pieceMoved++;
        if (pieceMoved == 2)
        {
            NextRound();
            pieceMoved = 0;
        }
        else
        {
            SetUpForMove();
        }

        yield return null;
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
            white_pieces[index].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
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
            black_pieces[index].transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
        //This part takes care of the UI part
    }

    void SpawnGreen(Vector3 spawn_pos, Quaternion rotation){
        Debug.Log("this is spawn green");
        //spawn the prefab at the given position
        GameObject instantiated = Instantiate(valid_sign, spawn_pos, rotation);
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

    // public void Rotate(GameObject cur_piece, int pos_index)
    // {
        
            
    //     if (horizontalPosition.Contains(pos_index))
    //     {
    //         //rotate 90 degree: horizontal
    //         cur_piece.transform.rotation = LiuboBoard.transform.rotation * Quaternion.Euler(0f, 0f, 0f);
    //     }
    //     else
    //     {
    //         //rotate back
    //         cur_piece.transform.rotation = LiuboBoard.transform.rotation * Quaternion.Euler(0f, 90f, 0f);
    //     }
        
    // }
    public void spawnStop(List<Vector3> poses){
        for(int i = 0; i < poses.Count; i++){
            Vector3 pos = poses[i];
            GameObject instantiated = Instantiate(stop_sign, pos, Quaternion.identity);
            int pos_index = bd.get_anchor_index(pos);
            if (!horizontalPosition.Contains(pos_index))
            {
                instantiated.transform.rotation = instantiated.transform.rotation * Quaternion.Euler(0f, 90f, 0f);
            }
            instantiated.transform.SetParent(canvas.transform);
            instantiated_stop_list.Add(instantiated);
        }
    }

    public void highLightFinal(List<Vector3> poses){
        Vector3 pos = poses[poses.Count - 1]; 
        for(int i = 0; i < instantiated_list.Count; i++){
            if(instantiated_list[i]){
                if(Vector3.Distance(instantiated_list[i].transform.position, pos) > 0.01){
                    instantiated_list[i].SetActive(false);
                }
            }
        }       
    }

    public void restoreFinals(){
        for(int i = 0; i < instantiated_list.Count; i++){
            if(instantiated_list[i]){
                instantiated_list[i].SetActive(true);
            }
        }
    }


    public void removeStops(){
        for(int i = 0; i < instantiated_stop_list.Count; i++){
            Destroy(instantiated_stop_list[i]);
        }
        instantiated_stop_list.Clear();
    }

    IEnumerator MovePieceAnimation(GameObject piece, List<int> path)
    {
        
        //TODO: spawn marks here
        // List<Vector3> poses = new List<Vector3>();
        // for(int i = 0; i < path.Count; i++){
        //     poses.Add(bd.GetBasePosition(path[i]));
        // }
        // spawnStop(poses);

        if (piece.CompareTag("Owl"))
        {
            var isTurnedToNormal = false;
            foreach (var nodeIndex in path)
            {
                var b = GetPieceOrientation(nodeIndex, !isTurnedToNormal);
                var a = bd.GetTopPosition(nodeIndex, !isTurnedToNormal);


                piece.transform.DORotateQuaternion(b, 1);
                yield return piece.transform.DOJump(a, 0.3f, 1, 1).WaitForCompletion();
                if (is_p1_turn && bd.whiteScoringNests.Contains(nodeIndex) ||
                    !is_p1_turn && bd.blackScoringNests.Contains(nodeIndex))
                {
                    isTurnedToNormal = true;
                    piece.transform.DOJump(bd.GetTopPosition(nodeIndex, !isTurnedToNormal), 0.1f, 1, 0.5f);
                    yield return piece.transform.DORotateQuaternion(GetPieceOrientation(nodeIndex, !isTurnedToNormal), 0.5f)
                        .WaitForCompletion();
                }
            }
        }
        else
        {
            var isTurnedToOwl = false;
            foreach (var nodeIndex in path)
            {
                var b = GetPieceOrientation(nodeIndex, isTurnedToOwl);
                var a = bd.GetTopPosition(nodeIndex, isTurnedToOwl);
                piece.transform.DORotateQuaternion(b, 1);
                yield return piece.transform.DOJump(a, 0.3f, 1, 1).WaitForCompletion();

                if (nodeIndex == bd.pond_index)
                {
                    isTurnedToOwl = true;
                    piece.transform.DOJump(bd.GetTopPosition(nodeIndex, isTurnedToOwl), 0.1f, 1, 0.5f);
                    OnTurningIntoOwl.Invoke();
                    yield return piece.transform.DORotateQuaternion(GetPieceOrientation(nodeIndex, isTurnedToOwl), 0.5f)
                        .WaitForCompletion();
                }
                
            }
        }
        // removeStops();
    }

    public void OnDestinationMouseEnter(Vector3 DestPos)
    {
        var destAnchor = bd.get_anchor_index(DestPos);
        var path = bd.final_paths.First(p => p[p.Count - 1] == destAnchor);
        // not_owl && isBecomingOwl || isOwl && !isBecomingNormal
        OnDestinationMouseEnterEvent.Invoke(path, willBeOwl(path, cur_piece.CompareTag("Owl")));
    }
    
    public void OnDestinationMouseExit()
    {
        OnDestinationMouseExitEvent.Invoke();
    }
    
    public Quaternion GetPieceOrientation(int nodeIndex, bool isOwl)
    {
        var ret = Quaternion.identity;
        if (horizontalPosition.Contains(nodeIndex))
        {
            //rotate 90 degree: horizontal
            ret *= LiuboBoard.transform.rotation * Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            //rotate back
            ret *= LiuboBoard.transform.rotation * Quaternion.Euler(0f, 90f, 0f);
        }
        if(isOwl){
            ret *= Quaternion.Euler(0f, 90f, 90f);
        }
        return ret;
    }

    private bool isPassingPond(List<int> path)
    {
        return path.Contains(bd.pond_index);
    }
    
    private bool isPassingNests(List<int> path) {
        return is_black_chosen ? bd.blackScoringNests.Overlaps(path) : bd.whiteScoringNests.Overlaps(path);
    }

    private bool willBeOwl(List<int> path, bool isCurrentlyOwl)
    {
        return isCurrentlyOwl && !isPassingNests(path) || !isCurrentlyOwl && isPassingPond(path);
    }
}

public class DestinationMouseEvent : UnityEvent<List<int>, bool>
{
    
}

public class TurnChangeEvent : UnityEvent<bool>
{
    
}




[Flags] public enum State
{
    Roll = 0,
    MoveOrPieceSelection = 1,
    MoveSelected = 2,
    PieceSelected = 4,
    PieceMoving = 8, 
}