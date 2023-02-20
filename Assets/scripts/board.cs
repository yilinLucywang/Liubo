using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;
public class board : MonoBehaviour
{   
    private static int total_poses = 37;
    private static int piece_number = 6;
    private Dictionary<int, int> anchor_2_index = new Dictionary<int, int> ();
    private Dictionary<int, int> index_2_anchor = new Dictionary<int, int> ();
    private List<int> final_poses =  new List<int>();
    //index rules: white piece index: 0-5; black piece index: 6-11 
    public List<List<int>> nodes = new List<List<int>>();
    public int pond_index = 0; 
    public int[,] normal_edges = new int[total_poses, total_poses];
    public int[,] owl_edges = new int[total_poses, total_poses];
    public int[,] pond_edges = new int[,]{{8,9},{9,13},{15,9},{14,9}};


    public bool is_p1_turn = true; 

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


        int cur_length = temp_owl_edges.GetLength(0);
        for(int i = 0; i < cur_length; i++){
            int first = temp_owl_edges[i,0]; 
            int second = temp_owl_edges[i,1];

            owl_edges[first,second] = 1;
            owl_edges[second, first] = 1; 

            normal_edges[first,second] = 1; 
            normal_edges[second,first] = 1;
        }
        //This is what common pieces cannot walk on
        int[,] temp_not_normal_edges = new int[,] {{10,12},{11,12},{0,2},{1,2},{30,29},{31,29},{21,23},{21,22}};
        cur_length = temp_not_normal_edges.GetLength(0);
        for(int i = 0; i < cur_length; i++){
            int first = temp_not_normal_edges[i,0]; 
            int second = temp_not_normal_edges[i,1];
            normal_edges[first,second] = 0;
            normal_edges[second, first] = 0;
        }

        cur_length = pond_edges.GetLength(0);
        for(int i = 0; i < cur_length; i++){
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

        //anchor_2_index

        for(int i = 0; i < anchors.Count; i++){
            string subject_string = anchors[i].name;
            string result_string = Regex.Match(subject_string,@"\d+").Value;
            int index = Int32.Parse(result_string);
            anchor_2_index[i] = index;
            index_2_anchor[index] = i; 
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

    public int get_anchor_index(Vector2 pos){
        int  index = -1; 
        for(int i = 0; i < anchors.Count; i++){
            Vector2 anchor_pos = anchors[i].transform.position; 
            if(Vector2.Distance(anchor_pos, pos) <= 8.0){
                index = anchor_2_index[i]; 
                return index;
            }
        }
        return index;
    }

    public List<Vector2> move(bool is_white, int piece_index, int step){
        int cur_pos = 0;
        if(is_white){
            cur_pos = white_pieces[piece_index];
        }
        else{
            cur_pos = black_pieces[piece_index];
        }

        if(cur_pos == -1){
            //starting pos: 11,1,30,22
            step = step - 1;
            if(is_white){
                //1,11
                //no way for this case to be owl
                final_poses.Clear();
                HashSet<int> visited_spot = new HashSet<int>();
                visited_spot.Add(1);
                normal_helper(1,step,visited_spot);
                visited_spot.Clear();
                visited_spot.Add(11);
                normal_helper(11,step,visited_spot);
            }else{
                //30,22
                //no way for this case to be owl
                final_poses.Clear();
                HashSet<int> visited_spot = new HashSet<int>();
                visited_spot.Add(30);
                normal_helper(30,step,visited_spot);
                visited_spot.Clear();
                visited_spot.Add(22);
                normal_helper(22,step,visited_spot);
            }
        }
        else{

            if(is_white){
                if(white_owls.Contains(piece_index)){
                    //This part searches in owl_edges
                    //has cycle, may cause some issue here in this dfs
                    final_poses.Clear();
                    HashSet<int> visited_spot = new HashSet<int>();
                    visited_spot.Add(cur_pos);
                    owl_helper(cur_pos,step,visited_spot);
                }
                else{
                    //This part searches in normal_edges
                    final_poses.Clear();
                    HashSet<int> visited_spot = new HashSet<int>(); 
                    visited_spot.Add(cur_pos);
                    normal_helper(cur_pos,step,visited_spot);
                }
            }
            else{
                if(black_owls.Contains(piece_index)){
                    //This part searches in owl_edges
                    final_poses.Clear();
                    HashSet<int> visited_spot = new HashSet<int>();
                    visited_spot.Add(cur_pos);
                    owl_helper(cur_pos,step,visited_spot);
                }
                else{
                    //This part searches in normal_edges
                    final_poses.Clear();
                    HashSet<int> visited_spot = new HashSet<int>();
                    visited_spot.Add(cur_pos);
                    normal_helper(cur_pos,step,visited_spot);
                }
            }
        }

        List<Vector2> final_positions = new List<Vector2> ();
        for(int i = 0; i < final_poses.Count; i++){
            int anchor_index = index_2_anchor[final_poses[i]];
            Vector2 position = anchors[anchor_index].transform.position;
            final_positions.Add(position);
        }
        return final_positions;
    }


    void normal_helper(int cur_pos, int step, HashSet<int> visited_spot){
        if(step < 1){
            final_poses.Add(cur_pos);
        }
        else{
            for(int i = 0; i < normal_edges.GetLength(0); i++){
                if(nodes[i].Count > 1 && isSameColor(i))
                {
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

                if(nodes[i].Count > 1 && isSameColor(i))
                {
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

    bool isSameColor(int i)
    {
        bool sameColor;

        if ((nodes[i][0] < 6 && nodes[i][0] < 6) || (nodes[i][0] > 5 && nodes[i][0] > 5))
        {
            sameColor = true;
        }
        else
        {
            sameColor = false;
        }
        return sameColor;
    }
}
