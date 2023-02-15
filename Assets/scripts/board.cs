using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class board : MonoBehaviour
{   
    private static int total_poses = 37;
    private static int piece_number = 6;
    private List<int> final_poses =  new List<int>();
    public List<List<int>> nodes = new List<List<int>>();
    public int pond_index = 0; 
    public int[,] normal_edges = new int[total_poses, total_poses];
    public int[,] owl_edges = new int[total_poses, total_poses];
    public int[,] pond_edges = new int[,]{{8,9},{9,13},{15,9},{14,9}};


    //public bool is_p1_turn = true; 

    //this maps piece index -> node_index, if not on board pieces[piece_index] = -1
    public List<int> white_pieces = new List<int>();
    public List<int> black_pieces = new List<int>();


    public List<GameObject> anchors = new List<GameObject>();


    //sets that stores the owl-piece indexs
    public HashSet<int> white_owls = new HashSet<int>();
    public HashSet<int> black_owls = new HashSet<int>();


    public int[] cur_rolls = new int[2];
    void Awake()
    {
        //-1 means no piece in current location
        for(int i = 0; i < total_poses; i++){
            //change to list 
            List<int> empty_pos =  new List<int>();
            nodes.Add(empty_pos);
        }


        for(int i = 0; i < total_poses; i++){
            for(int j = 0; j < total_poses; j++){
                normal_edges[i,j] = 0; 
                owl_edges[i,j] = 0;
            }
        }

        //add edges information to the edgeslist
        //1 means row[i] connects to col[j]
        //0 means row[i] doesn't connect to col[j]
        //This is the owl edges
        int[,] temp_owl_edges = new int[,] {{0,3},{1,0},{0,2},{1,2},{3,4},{4,10},{10,11},
        {11,12},{10,12},{11,19},{19,20},{20,22},{22,23},
        {23,24},{24,25},{25,31},{30,31},{29,30},{31,29},
        {30,35},{35,36},{1,36},{34,35},{33,34},{33,15},
        {14,27},{27,26},{24,26},{19,18},{18,17},{17,13},{3,5},
        {5,6},{6,8}};

        for(int i = 0; i < temp_owl_edges.Length; i++){
            int first = temp_owl_edges[i,0]; 
            int second = temp_owl_edges[i,1];

            owl_edges[first,second] = 1;
            owl_edges[second, first] = 1; 

            normal_edges[first,second] = 1; 
            normal_edges[second,first] = 1;
        }
        //This is what common pieces cannot walk on
        int[,] temp_not_normal_edges = new int[,] {{10,12},{11,12},{0,2},{1,2},{30,29},{31,29},{21,23},{21,22}};
        for(int i = 0; i < temp_not_normal_edges.Length; i++){
            int first = temp_not_normal_edges[i,0]; 
            int second = temp_not_normal_edges[i,1];
            normal_edges[first,second] = 0;
            normal_edges[second, first] = 0;
        }

        for(int i = 0; i < pond_edges.Length; i++){
            int first = pond_edges[i,0]; 
            int second = pond_edges[i,1];

            owl_edges[first,second] = 1;
            owl_edges[second, first] = 1; 

            normal_edges[first,second] = 1; 
            normal_edges[second,first] = 1;
        }

        for(int i = 0; i < piece_number; i++){
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


    List<int> move(bool is_white, int piece_index, int step){
        List<int> possible_poses = new List<int>();
        int cur_pos = 0;
        if(is_white){
            cur_pos = white_pieces[piece_index];
        }
        else{
            cur_pos = black_pieces[piece_index];
        }

        if(cur_pos == -1){
            throw new ArgumentException("current piece not on board; this move shouldn't be called; move it to the board first");
        }

        if(is_white){
            if(white_owls.Contains(piece_index)){
                //This part searches in owl_edges
                //has cycle, may cause some issue here in this dfs
                final_poses.Clear();
                HashSet<int> visited_spot = new HashSet<int>();
                owl_helper(cur_pos,step,visited_spot);
            }
            else{
                //This part searches in normal_edges
                final_poses.Clear();
                HashSet<int> visited_spot = new HashSet<int>(); 
                normal_helper(cur_pos,step,visited_spot);
            }
        }
        else{
            if(black_owls.Contains(piece_index)){
                //This part searches in owl_edges
                final_poses.Clear();
                HashSet<int> visited_spot = new HashSet<int>();
                owl_helper(cur_pos,step,visited_spot);
            }
            else{
                //This part searches in normal_edges
                final_poses.Clear();
                HashSet<int> visited_spot = new HashSet<int>();
                normal_helper(cur_pos,step,visited_spot);
            }
        }
        return final_poses;
    }


    void normal_helper(int cur_pos, int step, HashSet<int> visited_spot){
        if(step < 1){
            final_poses.Add(cur_pos);
        }
        else{
            for(int i = 0; i < normal_edges.GetLength(1); i++){
                if(nodes[i].Count > 1){
                    continue;
                }
                if(normal_edges[cur_pos,i] == 1){
                    if(!visited_spot.Contains(i)){
                        visited_spot.Add(i);
                        normal_helper(i,step - 1,visited_spot);
                        visited_spot.Remove(i);
                    }
                }
            }
        }
    }

    void owl_helper(int cur_pos, int  step, HashSet<int> visited_spot){
        if(step < 1){
            final_poses.Add(cur_pos);
        }
        else{
            for(int i = 0; i < owl_edges.GetLength(1); i++){
                if(nodes[i].Count > 1){
                    continue;
                }
                if(owl_edges[cur_pos,i] == 1){
                    if(!visited_spot.Contains(i)){
                        visited_spot.Add(i);
                        owl_helper(i,step - 1, visited_spot);
                        visited_spot.Remove(i);
                    }
                }
            }
        }
    }
}
