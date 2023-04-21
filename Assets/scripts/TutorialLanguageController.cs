using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLanguageController : MonoBehaviour
{
    public List<string> eButtons = new List<string>{ "Welcome to Liubo!"
                                                        ,"Quick Rules"
                                                        ,"The Game Screen"
                                                        ,"Rolling the Sticks"
                                                        ,"Your First Move"
                                                        ,"Moving Around the Board"
                                                        ,"Owls"
                                                        ,"Blockades"
                                                        ,"Contested Perches"
                                                        ,"Capturing"
                                                        ,"Scoring in Nests"
                                                        ,"Strategy"
                                                        }; 
    public List<List<string>> eContents = new List<List<string>>(); 
    public List<string> cButtons = new List<string>(); 
    public List<string> cContents = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
