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



    public List<List<int>> final_paths = new List<List<int>>();


    //try to store it here
    private List<int> path = new List<int>();

    //true stands for the final pose with that index will change the normal piece into owl
    private List<int> owl_poses = new List<int>();


    //index rules: white piece index: 0-5; black piece index: 6-11 
    public List<List<int>> nodes = new List<List<int>>();
    public int pond_index = 0; 
    public int[,] normal_edges = new int[total_poses, total_poses];
    public int[,] white_owl_edges = new int[total_poses, total_poses];
    public int[,] black_owl_edges = new int[total_poses, total_poses];


    public bool is_p1_turn = true; 

    //this maps piece index -> node_index, if not on board pieces[piece_index] = -1
    public List<int> white_pieces = new List<int>();
    public List<int> black_pieces = new List<int>();


    public List<GameObject> anchors = new List<GameObject>();


    //sets that stores the owl-piece indexs
    public HashSet<int> white_owls = new HashSet<int>();
    public HashSet<int> black_owls = new HashSet<int>();

    //Max valid distance when comparing validMove positions to anchor positions
    public float max_anchor_distance = 0.02f;

    
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
                white_owl_edges[i,j] = 0;
                black_owl_edges[i,j] = 0;
            }
        }

        //add edges information to the edgeslist
        //1 means row[i] connects to col[j]
        //0 means row[i] doesn't connect to col[j]
        //This is the owl edges
        int[,] temp_owl_edges = new int[,] {{0,1},{0,4},{0,2},{1,2},{1,36},{36,30},{30,31},{30,29},{29,31},{31,25},
        {25,23},{23,22},{23,21},{21,22},{22,20},{20,11},{12,11},{11,10},{10,12},{10,4},{3,4},{3,5},{5,6},{6,8},{8,9},
        {35,36},{35,34},{34,33},{33,15},{15,9},{25,24},{24,26},{26,27},{27,14},{14,9},{20,19},{19,18},{18,17},
        {17,13},{13,9}};

        int cur_length = temp_owl_edges.GetLength(0);
        for(int i = 0; i < cur_length; i++){
            int first = temp_owl_edges[i,0]; 
            int second = temp_owl_edges[i,1];

            white_owl_edges[first,second] = 1;
            white_owl_edges[second, first] = 1; 

            black_owl_edges[first,second] = 1;
            black_owl_edges[second, first] = 1;

            normal_edges[first,second] = 1; 
            normal_edges[second,first] = 1;
        }

        //This is what common pieces cannot walk on
        int[,] temp_not_normal_edges = new int[,] {{10,12},{11,12},{0,2},{1,2},{30,29},{31,29},{23,21},{22,21}};
        cur_length = temp_not_normal_edges.GetLength(0);
        for(int i = 0; i < cur_length; i++){
            int first = temp_not_normal_edges[i,0]; 
            int second = temp_not_normal_edges[i,1];
            normal_edges[first,second] = 0;
            //This lets normal pieces leave nests, but not enter them
            normal_edges[second, first] = 1;

            white_owl_edges[first,second] = 1;
            white_owl_edges[second, first] = 1; 

            black_owl_edges[first,second] = 1;
            black_owl_edges[second, first] = 1; 
        }

        //white bottom
        int[,] white_not_normal_edges = new int[,] {{30,29},{31,29},{21,23},{21,22}};
        int[,] black_not_normal_edges = new int[,] {{10,12},{11,12},{0,2},{1,2}};
        cur_length = white_not_normal_edges.GetLength(0);
        for(int i = 0; i < cur_length; i++){
            int first = white_not_normal_edges[i,0]; 
            int second = white_not_normal_edges[i,1];

            white_owl_edges[first,second] = 1;
            white_owl_edges[second, first] = 1; 

            black_owl_edges[first,second] = 0;
            black_owl_edges[second, first] = 0; 
        }

        cur_length = black_not_normal_edges.GetLength(0);
        for(int i = 0; i < cur_length; i++){
            int first = black_not_normal_edges[i,0]; 
            int second = black_not_normal_edges[i,1];

            white_owl_edges[first,second] = 0;
            white_owl_edges[second, first] = 0; 

            black_owl_edges[first,second] = 1;
            black_owl_edges[second, first] = 1; 
        }

        for(int i = 0; i < piece_number; i++){
            white_pieces.Add(-1); 
            black_pieces.Add(-1);
        }
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

    public int get_anchor_index(Vector3 pos){
        int  min_index = -1; 
        float min_dist = 100000.0f;
        for(int i = 0; i < anchors.Count; i++){
            Vector3 anchor_pos = anchors[i].transform.position;
            if (Math.Sqrt((anchor_pos.x - pos.x)*(anchor_pos.x - pos.x) + (anchor_pos.z - pos.z)*(anchor_pos.z - pos.z)) < min_dist)
            { 
                min_index = anchor_2_index[i]; 
                min_dist = (float)Math.Sqrt((anchor_pos.x - pos.x)*(anchor_pos.x - pos.x) + (anchor_pos.z - pos.z)*(anchor_pos.z - pos.z));
            }
        }
        return min_index;
    }

    List<T> CreateList<T>(params T[] values)
    {
        return new List<T>(values);
    }

    public List<Vector3> move(bool is_white, int piece_index, int step){
        final_paths.Clear();
        if(step < 1){
            return new List<Vector3> ();
        }
        Score score = gameObject.GetComponent<Score>();
        int cur_pos = 0;
        if(is_white){
            cur_pos = white_pieces[piece_index];
        }
        else{
            cur_pos = black_pieces[piece_index];
        }
        //Debug.Log("cur_pos = " + cur_pos);

        if (cur_pos == -1){
            //starting pos: 11,0,30,23
            step = step - 1;
            if(is_white){
                //0,11
                //no way for this case to be owl
                final_poses.Clear();
                owl_poses.Clear();
                HashSet<int> visited_spot = new HashSet<int>();
                visited_spot.Add(0);
                List<int> p_1 = CreateList(0);
                normal_helper(0,step,visited_spot,p_1);
                visited_spot.Clear();
                visited_spot.Add(11);
                List<int> p_2 = CreateList(11);
                normal_helper(11,step,visited_spot,p_2);
                Debug.Log("Is this right?");
                Debug.Log(final_paths.Count);
                for(int k = 0; k < final_paths.Count; k++){
                    string pathString = "";
                    for(int j = 0; j < final_paths[k].Count; j++){
                        pathString += final_paths[k][j].ToString();
                }
                Debug.Log(pathString);
        }
            }else{
                //30,23
                //no way for this case to be owl
                final_poses.Clear();
                owl_poses.Clear();
                HashSet<int> visited_spot = new HashSet<int>();
                path.Clear();
                path.Add(30);
                visited_spot.Add(30);
                normal_helper(30,step,visited_spot,path);
                visited_spot.Clear();
                visited_spot.Add(23);
                path.Clear();
                path.Add(23);
                normal_helper(23,step,visited_spot,path);
            }
        }
        else{
            if(is_white){
                //white owls
                string piece_name = (piece_index+6).ToString();
                GameObject piece = GameObject.Find(piece_name);
                if (piece.CompareTag("Owl"))
                {
                    //This part searches in owl_edges
                    //has cycle, may cause some issue here in this dfs
                    final_poses.Clear();
                    owl_poses.Clear();
                    HashSet<int> visited_spot = new HashSet<int>();
                    visited_spot.Add(cur_pos);
                    path.Clear();
                    path.Add(cur_pos);
                    white_owl_helper(cur_pos,step,visited_spot,path);
                }
                else{
                    //This part searches in normal_edges
                    final_poses.Clear();
                    owl_poses.Clear();
                    HashSet<int> visited_spot = new HashSet<int>(); 
                    visited_spot.Add(cur_pos);
                    path.Clear();
                    path.Add(cur_pos);
                    normal_helper(cur_pos,step,visited_spot,path);
                }
            }
            else{
                //black_owls
                string piece_name = piece_index.ToString();
                GameObject piece = GameObject.Find(piece_name);
                if (piece.CompareTag("Owl"))
                {
                    //This part searches in owl_edges
                    final_poses.Clear();
                    owl_poses.Clear();
                    HashSet<int> visited_spot = new HashSet<int>();
                    visited_spot.Add(cur_pos);
                    path.Clear();
                    path.Add(cur_pos);
                    black_owl_helper(cur_pos,step,visited_spot,path);
                }
                else{
                    //This part searches in normal_edges
                    final_poses.Clear();
                    owl_poses.Clear();
                    HashSet<int> visited_spot = new HashSet<int>();
                    visited_spot.Add(cur_pos);
                    path.Clear();
                    path.Add(cur_pos);
                    normal_helper(cur_pos,step,visited_spot,path);
                }
            }
        }

        List<Vector3> final_positions = new List<Vector3> ();
        for(int i = 0; i < final_poses.Count; i++){
            int anchor_index = index_2_anchor[final_poses[i]];
            Vector3 position = anchors[anchor_index].transform.position;
            //only add to pissible moves when there are available position
            if(nodes[final_poses[i]].Count <= 2){
                final_positions.Add(position);
            }
        }


        // for(int k = 0; k < final_paths.Count; k++){
        //     string pathString = "";
        //     for(int j = 0; j < final_paths[k].Count; j++){
        //         pathString += final_paths[k][j].ToString();
        //     }
        //     Debug.Log(pathString);
        // }

        return final_positions;
    }


    public bool is_becoming_owl(int index){
        for(int i = 0; i < owl_poses.Count; i++){
            if(owl_poses[i] == index){
                return true;
            }
        }
        return false;
    }

    public List<List<int>> get_paths(){
        return final_paths;
    }

    void normal_helper(int cur_pos, int step, HashSet<int> visited_spot, List<int> path_1){
        if(step < 1){
            final_poses.Add(cur_pos);
            //path.Add(cur_pos);
            List<int> path_2 = new List<int>();
            for(int i = 0; i < path_1.Count; i++){
                int val = path_1[i];
                path_2.Add(val);
            }
            final_paths.Add(path_2);
            Debug.Log("hi");
            Debug.Log(final_paths.Count);
            for(int k = 0; k < final_paths.Count; k++){
                string pathString = "";
                for(int j = 0; j < final_paths[k].Count; j++){
                    pathString += final_paths[k][j].ToString();
                }
                Debug.Log(pathString);
            }
            Debug.Log("end");
            if(visited_spot.Contains(9)){
                owl_poses.Add(cur_pos);
            }
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
                        path_1.Add(i);
                        normal_helper(i,step - 1,visited_spot,path_1);
                        visited_spot.Remove(i);
                        path_1.RemoveAt(path_1.Count - 1);
                    }
                }
            }
        }
    }

    void white_owl_helper(int cur_pos, int  step, HashSet<int> visited_spot, List<int> path){
        if(step < 1){
            final_poses.Add(cur_pos);
            final_paths.Add(path);
            if(visited_spot.Contains(9)){
                owl_poses.Add(cur_pos);
            }
        }
        else{
            for(int i = 0; i < white_owl_edges.GetLength(1); i++){

                if(nodes[i].Count > 1 && isSameColor(i))
                {
                    continue;
                }
                
                if(white_owl_edges[cur_pos,i] == 1){
                    if(!visited_spot.Contains(i)){
                        visited_spot.Add(i);
                        path.Add(i);
                        white_owl_helper(i,step - 1, visited_spot,path);
                        visited_spot.Remove(i);
                        path.RemoveAt(path.Count - 1);

                    }
                }
            }
        }
    }

    void black_owl_helper(int cur_pos, int  step, HashSet<int> visited_spot, List<int> path){
        if(step < 1){
            final_poses.Add(cur_pos);
            final_paths.Add(path);
            if(visited_spot.Contains(9)){
                owl_poses.Add(cur_pos);
            }
        }
        else{
            for(int i = 0; i < black_owl_edges.GetLength(1); i++){

                if(nodes[i].Count > 1 && isSameColor(i))
                {
                    continue;
                }
                
                if(black_owl_edges[cur_pos,i] == 1){
                    if(!visited_spot.Contains(i)){
                        visited_spot.Add(i);
                        path.Add(i);
                        black_owl_helper(i,step - 1, visited_spot,path);
                        visited_spot.Remove(i);
                        path.RemoveAt(path.Count - 1);
                    }
                }
            }
        }
    }

    public bool isSameColor(int i)
    {
        bool sameColor;

        if ((nodes[i][0] < 6 && nodes[i][1] < 6) || (nodes[i][0] > 5 && nodes[i][1] > 5))
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
