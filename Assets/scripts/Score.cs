using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour
{
    
    public GameData gameData;
    public bool hasDiffColor;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


   
    public void CalculateScore(Vector3 position, int chosen_piece, bool is_black_chosen, bool is_owl)
    {
        string chosen_piece_name = chosen_piece.ToString();
        GameObject cur_piece = GameObject.Find(chosen_piece_name);
        //cur_piece.transform.position = position;
        board bd = gameObject.GetComponent<board>();
        GameState gamestate = gameObject.GetComponent<GameState>();
        int pos_index = bd.get_anchor_index(position);
        //
        int diffColorIndex ;
        UI UI = gameObject.GetComponent<UI>();
        //diffColorIndex = findDifferentColor(pos_index);
        //bd.nodes[pos_index].Add(chosen_piece);


        //if (pos_index == 9)
        if(is_owl)
        {
            cur_piece.tag = "Owl";
            cur_piece.transform.rotation = cur_piece.transform.rotation * Quaternion.Euler(0f, 0f, 90f);
            cur_piece.transform.position += new Vector3(0f, 0.15f, 0f);
        }

        if (is_black_chosen)
        {
            Debug.Log("Black Turn");
            bd.black_pieces[chosen_piece] = pos_index;
            if (cur_piece.CompareTag("Owl"))
            {
                Debug.Log("black owl move");
                //black owl land on nest
                if ((pos_index == 2) || (pos_index == 12))//(pos_index == 21)|| (pos_index == 29)
                {
                    gameData.black_score += 2;
                    changeTagOwl(cur_piece, "Normal");
                    RotateBackToNorm(cur_piece);
                }
            }
                // if there is already a piece on there
             diffColorIndex = findDifferentColor(pos_index, is_black_chosen);
            if (hasDiffColor)
            {
                Debug.Log("hasDiffColor");
                //black owl
                if (cur_piece.CompareTag("Owl") )
                {
                    //black owl eat white normal
                    if (findTag(bd.nodes[pos_index][diffColorIndex]) == "Normal")
                    {
                        gameData.black_score++;
                        Debug.Log("Black owl get white normal");
                        //remove different color piece
                        gamestate.RemovePiece(bd.nodes[pos_index][diffColorIndex] - 6, true);
                    }
                    //2 black owl eat white owl
                    else if (findTag(bd.nodes[pos_index][diffColorIndex]) == "Owl" && (bd.nodes[pos_index].Count > 1))
                    {
                        gameData.black_score += 3;
                        changeTag(bd.nodes[pos_index][diffColorIndex], "Normal");

                        RotateBackToNorm(cur_piece);
                        Debug.Log("2 Black owl get white owl");
                        //remove different color piece
                        gamestate.RemovePiece(bd.nodes[pos_index][diffColorIndex] - 6, true);
                    }
                }
                //black normal
                else if (cur_piece.CompareTag("Normal") )
                {
                    if (findTag(bd.nodes[pos_index][diffColorIndex]) == "Owl")
                    {
                        gameData.black_score += 3;
                        Debug.Log("Black normal get white owl");
                        RotateBackToNorm(cur_piece);
                        //remove different color piece
                        gamestate.RemovePiece(bd.nodes[pos_index][diffColorIndex] - 6, true);
                    }
                    Debug.Log("index"+ bd.nodes[pos_index].Count);
                    if ( (bd.nodes[pos_index].Count > 1)&&(findTag(bd.nodes[pos_index][diffColorIndex]) == "Normal"))
                    {
                        Debug.Log("2 b normal get 1 w normal ");
                        gameData.black_score++;
                        //remove different color piece
                        gamestate.RemovePiece(bd.nodes[pos_index][diffColorIndex] - 6, true);
                    }
                }
                
            }

        }
        else//white chosen
        {
            bd.white_pieces[chosen_piece-6] = pos_index;

            if (cur_piece.CompareTag("Owl"))
            {
                Debug.Log("white owl move");
                //white owl land on nest
                if ((pos_index == 21) || (pos_index == 29))
                {
                    gameData.black_score += 2;
                    changeTagOwl(cur_piece, "Normal");
                    //RotateBackToNorm(cur_piece);
                }
            }
            // if there is already a piece on there
            diffColorIndex = findDifferentColor(pos_index, is_black_chosen);
            if (hasDiffColor)
            {
                //black owl
                if (cur_piece.CompareTag("Owl"))
                {
                    //white owl eat black normal
                    if (findTag(bd.nodes[pos_index][diffColorIndex]) == "Normal")
                    {
                        gameData.white_score++;
                        Debug.Log("white owl get black normal");
                        //remove different color piece
                        gamestate.RemovePiece(bd.nodes[pos_index][diffColorIndex], false);
                    }
                    //2 black owl eat white owl
                    else if (findTag(bd.nodes[pos_index][diffColorIndex]) == "Owl" && (bd.nodes[pos_index].Count > 1))
                    {
                        gameData.white_score += 3;
                        changeTag(bd.nodes[pos_index][diffColorIndex], "Normal");

                        //RotateBackToNorm(cur_piece);
                        Debug.Log("2 white owl get black owl");
                        //remove different color piece
                        gamestate.RemovePiece(bd.nodes[pos_index][diffColorIndex], false);
                    }
                }
                //black normal
                else if (cur_piece.CompareTag("Normal"))
                {
                    if (findTag(bd.nodes[pos_index][diffColorIndex]) == "Owl")
                    {
                        gameData.white_score += 3;
                        Debug.Log("white normal get black owl");
                        //RotateBackToNorm(cur_piece);
                        //remove different color piece
                        gamestate.RemovePiece(bd.nodes[pos_index][diffColorIndex], false);
                    }
                    if ((bd.nodes[pos_index].Count > 1) && (findTag(bd.nodes[pos_index][diffColorIndex]) == "Normal"))
                    {
                        Debug.Log("2 w normal get 1 b normal ");
                        gameData.white_score++;
                        //remove different color piece
                        gamestate.RemovePiece(bd.nodes[pos_index][diffColorIndex], false);
                    }
                }
                
            }
        }
        //update UI
        UI.ChangeScore();
    }
    public string findTag(int index)
    {
        string piece_name = index.ToString();
        GameObject piece = GameObject.Find(piece_name);
        string tagName = piece.tag;
        return tagName;
    }
    public void changeTagOwl(GameObject cur_piece, string newTag)
    {
        //string piece_name = index.ToString();
        //GameObject piece = GameObject.Find(piece_name);
        cur_piece.tag = newTag;
        return;
    }

    public void changeTag(int index, string newTag)
    {
        string piece_name = index.ToString();
        GameObject piece = GameObject.Find(piece_name);
        piece.tag = newTag;
        return;
    }
    public int findDifferentColor(int pos_index, bool is_black_chosen)
    {
        board bd = gameObject.GetComponent<board>();
        //Debug.Log(bd.nodes[pos_index].Count);
        if (bd.nodes[pos_index].Count > 0)
        {
            if (is_black_chosen)
            {
                for (var i = 0; i < bd.nodes[pos_index].Count; i++)
                {
                    if (bd.nodes[pos_index][i] > 5)
                    {
                        hasDiffColor = true;
                        return i;
                    }
                }
            }

            else
            {
                for (var i = 0; i < bd.nodes[pos_index].Count; i++)
                {
                    if (bd.nodes[pos_index][i] < 6)
                    {
                        hasDiffColor = true;
                        return i;
                    }
                }
            }
            hasDiffColor = false;
            return 0;
        }
        else
        {
            hasDiffColor = false;
            return 0;
        }
    }
    public void RotateBackToNorm(GameObject cur_piece)
    {
        cur_piece.transform.rotation = cur_piece.transform.rotation * Quaternion.Euler(0f, 0f, 90f);
        cur_piece.transform.position -= new Vector3(0f, 0.15f, 0f);
    }
}
