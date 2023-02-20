using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour
{
    //public int white_score = 0, black_score = 0;
    public GameData gameData;
    private static int total_poses = 37;
    private static int piece_number = 6;
    private List<int> final_poses = new List<int>();
    public List<List<int>> nodes = new List<List<int>>();
    public int pond_index = 0;
    public int[,] normal_edges = new int[total_poses, total_poses];
    public int[,] owl_edges = new int[total_poses, total_poses];
    public int[,] pond_edges = new int[,] { { 8, 9 }, { 9, 13 }, { 15, 9 }, { 14, 9 } };


    public bool is_p1_turn = true;

    //this maps piece index -> node_ind ex, if not on board pieces[piece_index] = -1
    public List<int> white_pieces = new List<int>();
    public List<int> black_pieces = new List<int>();


    //sets that stores the owl-piece indexs
    public HashSet<int> white_owls = new HashSet<int>();
    public HashSet<int> black_owls = new HashSet<int>();

    //
    public int dice_1 = -1;
    public int dice_2 = -1;

    public Text num_1_text;
    public Text num_2_text;

    public bool is_black_chosen = true;
    public int chosen_piece = -1;


    //prefab to spawn
    public GameObject valid_sign;
    public GameObject canvas;

    private int cur_step = 0;
    private List<GameObject> instantiated_list = new List<GameObject>();


    public int[] cur_rolls = new int[2];
    void Awake()
    {
        //-1 means no piece in current location
        for (int i = 0; i < total_poses; i++)
        {
            nodes.Add(-1);
        }


        for (int i = 0; i < total_poses; i++)
        {
            for (int j = 0; j < total_poses; j++)
            {
                normal_edges[i, j] = 0;
                owl_edges[i, j] = 0;
            }
        }

        //add edges information to the edgeslist
        //1 means row[i] connects to col[j]
        //0 means row[i] doesn't connect to col[j]
        //This is the owl edges
        int[,] temp_owl_edges = new int[,] {{0,3},{1,0},{0,2},{1,2},{3,4},{4,10},{10,11},
        {11,12},{10,12},{11,19},{19,20},{20,22},{22,23},
        {23,24},{24,25},{25,31},{30,31},{29,30},{31,29},
        {30,35},{35,36},{1,36},{34,35},{33,34},{32,33},{32,15},
        {14,28},{28,27},{27,26},{24,26},{19,18},{18,17},{17,16},{13,16},{3,5},
        {5,6},{6,7},{7,8}};

        for (int i = 0; i < temp_owl_edges.Length; i++)
        {
            int first = temp_owl_edges[i, 0];
            int second = temp_owl_edges[i, 1];

            owl_edges[first, second] = 1;
            owl_edges[second, first] = 1;

            normal_edges[first, second] = 1;
            normal_edges[second, first] = 1;
        }
        //This is what common pieces cannot walk on
        int[,] temp_not_normal_edges = new int[,] { { 10, 12 }, { 11, 12 }, { 0, 2 }, { 1, 2 }, { 30, 29 }, { 31, 29 }, { 21, 23 }, { 21, 22 } };
        for (int i = 0; i < temp_not_normal_edges.Length; i++)
        {
            int first = temp_not_normal_edges[i, 0];
            int second = temp_not_normal_edges[i, 1];
            normal_edges[first, second] = 0;
            normal_edges[second, first] = 0;
        }

        for (int i = 0; i < pond_edges.Length; i++)
        {
            int first = pond_edges[i, 0];
            int second = pond_edges[i, 1];

            owl_edges[first, second] = 1;
            owl_edges[second, first] = 1;

            normal_edges[first, second] = 1;
            normal_edges[second, first] = 1;
        }

        for (int i = 0; i < piece_number; i++)
        {
            white_pieces.Add(-1);
            black_pieces.Add(-1);
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    List<int> move(bool is_white, int piece_index, int step)
    {
        List<int> possible_poses = new List<int>();
        int cur_pos = 0;
        if (is_white)
        {
            cur_pos = white_pieces[piece_index];
        }
        else
        {
            cur_pos = black_pieces[piece_index];
        }

        if (cur_pos == -1)
        {
            throw new ArgumentException("current piece not on board; this move shouldn't be called; move it to the board first");
        }

        if (is_white)
        {
            if (white_owls.Contains(piece_index))
            {
                //This part searches in owl_edges
                //has cycle, may cause some issue here in this dfs
                final_poses.Clear();
                owl_helper(cur_pos, step);

                //calculate score
                int last = final_poses[final_poses.Count - 1];
                if (black_pieces.Contains(last))
                {
                    //owl -> 1 normal opponment || (1 normal opponment && 1 normal friend)
                    //send back the normal black piece on the edge
                    int normal_index = black_pieces.IndexOf(last);
                    black_pieces[normal_index] = -1;
                    //add 3 point if eat black owl, 1 point blakc normal
                    if (black_owls.Contains(last))
                    {
                        gameData.white_score += 3;
                    }
                    else
                    {
                        gameData.white_score++;
                    }
                }
            }
            else
            {
                //This part searches in normal_edges
                final_poses.Clear();
                normal_helper(cur_pos, step);

                //calculate score
                int last = final_poses[final_poses.Count - 1];
                if (black_pieces.Contains(last))
                {
                    //normal -> 1 owl opponment || (1 normal opponment && 1 normal friend)
                    //send back the normal black piece on the edge
                    int normal_index = black_pieces.IndexOf(last);
                    black_pieces[normal_index] = -1;
                    //add 3 point if eat black owl, 1 point blakc normal
                    if (black_owls.Contains(last))
                    {
                        gameData.white_score += 3;
                    }
                    else
                    {
                        gameData.white_score++;
                    }

                }
            }
        }
        else
        {
            if (black_owls.Contains(piece_index))
            {
                //This part searches in owl_edges
                final_poses.Clear();
                owl_helper(cur_pos, step);

                //calculate score
                int last = final_poses[final_poses.Count - 1];
                if (white_pieces.Contains(last))
                {
                    //owl -> 1 normal opponment || (1 normal opponment && 1 normal friend)
                    //send back the normal black piece on the edge
                    int normal_index = white_pieces.IndexOf(last);
                    white_pieces[normal_index] = -1;
                    //add 3 point if eat black owl, 1 point blakc normal
                    if (white_owls.Contains(last))
                    {
                        gameData.black_score += 3;
                    }
                    else
                    {
                        gameData.black_score++;
                    }

                }
            }
            else
            {
                //This part searches in normal_edges
                final_poses.Clear();
                normal_helper(cur_pos, step);

                //calculate score
                int last = final_poses[final_poses.Count - 1];
                if (white_pieces.Contains(last))
                {
                    //normal -> 1 owl opponment || (1 normal opponment && 1 normal friend)
                    //send back the normal black piece on the edge
                    int normal_index = white_pieces.IndexOf(last);
                    white_pieces[normal_index] = -1;
                    //add 3 point if eat black owl, 1 point blakc normal
                    if (white_owls.Contains(last))
                    {
                        gameData.black_score += 3;
                    }
                    else
                    {
                        gameData.black_score++;
                    }

                }

            }
        }
        return final_poses;
    }


    void normal_helper(int cur_pos, int step)
    {
        if (step < 1)
        {
            final_poses.Add(cur_pos);
        }
        else
        {
            for (int i = 0; i < normal_edges.GetLength(1); i++)
            {
                if (normal_edges[cur_pos, i] == 1)
                {
                    normal_helper(i, step - 1);
                }
            }
        }
    }

    void owl_helper(int cur_pos, int step)
    {
        if (step < 1)
        {
            final_poses.Add(cur_pos);
        }
        else
        {
            for (int i = 0; i < owl_edges.GetLength(1); i++)
            {
                if (owl_edges[cur_pos, i] == 1)
                {
                    owl_helper(i, step - 1);
                }
            }
        }
    }

    public void MovePiece(Vector2 position)
    {
        //TODO: 1. move piece
        if (!is_black_chosen)
        {
            chosen_piece = chosen_piece + 6;
        }

        string chosen_piece_name = chosen_piece.ToString();
        GameObject cur_piece = GameObject.Find(chosen_piece_name);
        cur_piece.transform.position = position;
        board bd = gameObject.GetComponent<board>();
        int pos_index = bd.get_anchor_index(position);
        bd.nodes[pos_index].Add(chosen_piece);

        if (!is_black_chosen)
        {
            chosen_piece = chosen_piece - 6;
        }
        Debug.Log(pos_index);
        Debug.Log("num name: ");
        Debug.Log(chosen_piece);
        if (is_black_chosen)
        {
            bd.black_pieces[chosen_piece] = pos_index;
        }
        else
        {
            bd.white_pieces[chosen_piece] = pos_index;
        }

    }

    public void CalculateScore(Vector2 position, int chosen_piece)
    {
        string chosen_piece_name = chosen_piece.ToString();
        GameObject cur_piece = GameObject.Find(chosen_piece_name);
        cur_piece.transform.position = position;
        board bd = gameObject.GetComponent<board>();
        int pos_index = bd.get_anchor_index(position);
        bd.nodes[pos_index].Add(chosen_piece);

        int diffColorIndex;

        if (pos_index == 9)
        {
            cur_piece.tag = "Owl";
        }

        if (is_black_chosen)
        {
            bd.black_pieces[chosen_piece] = pos_index;
            if (cur_piece.tag == "Normal" && (bd.nodes[pos_index].Count == 1))
            {
                return;
            }
            else if (cur_piece.tag == "Owl" )
            {
                //black owl  
                diffColorIndex = findDifferentColor(pos_index);
                if (bd.nodes[pos_index][0] > 5)
                {
                    //white owl
                    if (findTag(bd.nodes[pos_index][diffColorIndex]) == "Owl")
                    {
                        gameData.black_score += 3;
                    }
                    //white normal
                    else
                    {
                        gameData.black_score++;
                    }

                    bd.nodes[pos_index].Remove(diffColorIndex);

                }
            }
        }

    }
    public string findTag(int index)
    {
        string piece_name = index.ToString();
        GameObject piece = GameObject.Find(piece_name);
        string tagName = piece.tag;
        return tagName;
    }
    public int findDifferentColor(int pos_index)
    {
        board bd = gameObject.GetComponent<board>();
        if (is_black_chosen)
        {
            //if white or only have one index so assume first is white
            if (bd.nodes[pos_index][0] > 5 || (bd.nodes[pos_index].Count == 1))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        else
        {
            if (bd.nodes[pos_index][0] < 6 || (bd.nodes[pos_index].Count == 1))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        
        
    }
}
