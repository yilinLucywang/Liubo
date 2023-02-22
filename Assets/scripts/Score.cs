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


   
    public void CalculateScore(Vector2 position, int chosen_piece, bool is_black_chosen)
    {
        string chosen_piece_name = chosen_piece.ToString();
        GameObject cur_piece = GameObject.Find(chosen_piece_name);
        cur_piece.transform.position = position;
        board bd = gameObject.GetComponent<board>();
        int pos_index = bd.get_anchor_index(position);
        //
        int diffColorIndex ;
        UI UI = gameObject.GetComponent<UI>();
        //diffColorIndex = findDifferentColor(pos_index);
        //bd.nodes[pos_index].Add(chosen_piece);


        if (pos_index == 9)
        {
            cur_piece.tag = "Owl";
        }

        if (is_black_chosen)
        {
            Debug.Log("Black Turn");
            bd.black_pieces[chosen_piece] = pos_index;
            if (cur_piece.CompareTag("Owl"))
            {
                //black owl land on nest
                if ((pos_index == 2) || (pos_index == 12))//(pos_index == 21)|| (pos_index == 29)
                {
                    gameData.black_score += 2;
                    changeTag(bd.nodes[pos_index].Count - 1, "Normal");
                }
            }
                // if there is already a piece on there
            if (bd.nodes[pos_index].Count > 0 && hasDiffColor)
            {
                diffColorIndex = findDifferentColor(pos_index, is_black_chosen);
                //black pieces eat white owl
                if (findTag(bd.nodes[pos_index][diffColorIndex]) == "Owl")
                {
                    gameData.black_score += 3;
                    changeTag(bd.nodes[pos_index][diffColorIndex], "Normal");
                }
                else if (cur_piece.CompareTag("Owl") )
                {
                    //black owl eat white normal
                    if (findTag(bd.nodes[pos_index][diffColorIndex]) == "Normal")
                    {
                        gameData.black_score++;
                        Debug.Log("Black owl get white normal");
                    }
                }
                else if (cur_piece.CompareTag("Normal")&& (bd.nodes[pos_index].Count > 1))
                {
                    if (findTag(bd.nodes[pos_index][diffColorIndex]) == "Normal")
                    {
                        gameData.black_score++;
                    }
                }
                    //remove different color piece
                bd.nodes[pos_index].Remove(diffColorIndex);
            }
        }
        else//white chosen
        {
            bd.white_pieces[chosen_piece-6] = pos_index;
            if (cur_piece.CompareTag("Owl"))
            {
                //white owl land on nest
                if ((pos_index == 21) || (pos_index == 29))
                {
                    gameData.black_score += 2;
                    changeTag(bd.nodes[pos_index].Count - 1, "Normal");
                }
            }
            // if there is already a piece on there
            if (bd.nodes[pos_index].Count > 0 && hasDiffColor)
            {
                diffColorIndex = findDifferentColor(pos_index, is_black_chosen);
                //white pieces eat black owl
                if (findTag(bd.nodes[pos_index][diffColorIndex]) == "Owl")
                {
                    gameData.white_score += 3;
                    changeTag(bd.nodes[pos_index][diffColorIndex], "Normal");
                }
                else if (cur_piece.CompareTag("Owl"))
                {
                    //white owl eat black normal
                    if (findTag(bd.nodes[pos_index][diffColorIndex]) == "Normal")
                    {
                        gameData.white_score++;
                    }
                }
                //two white normal eat black normal
                else if (cur_piece.CompareTag("Normal") && (bd.nodes[pos_index].Count > 1))
                {
                    if (findTag(bd.nodes[pos_index][diffColorIndex]) == "Normal")
                    {
                        gameData.white_score++;
                    }
                }
                //remove different color piece
                bd.nodes[pos_index].Remove(diffColorIndex);
            }
        }
        //update UI
        UI.ChangeScore();
    }
    public string findTag(int index)
    {
        Debug.Log(index);
        string piece_name = index.ToString();
        GameObject piece = GameObject.Find(piece_name);
        string tagName = piece.tag;
        return tagName;
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
        if (is_black_chosen)
        {
            //if white or only have one index so assume first is white
            if (bd.nodes[pos_index][0] > 5)
            {
                hasDiffColor = true;
                return 0;
            }
            else if (bd.nodes[pos_index][1] > 5)
            {
                hasDiffColor = true;
                return 1;
            }
            else
            {
                hasDiffColor = false;
                return 0;
            }

        }
        else
        {
            if (bd.nodes[pos_index][0] < 6 )
            {
                hasDiffColor = true;
                return 0;
            }
            else if (bd.nodes[pos_index][1] < 6)
            {
                hasDiffColor = true;
                return 1;
            }
            else
            {
                hasDiffColor = false;
                return 0;
            }
        }
  
    }
}
