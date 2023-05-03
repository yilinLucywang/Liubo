using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using UnityEngine.UI;
using TMPro;

public class WriteToGameObject : MonoBehaviour
{
    public List<GameObject> sectionList;
    // Start is called before the first frame update
    void Awake()
    {
        string path = @"Assets\scripts\tutorialText.csv";
        string[] lines = System.IO.File.ReadAllLines(path);
        int rowCount = 0;
        string text = "";
        foreach(string line in lines)
        {
            text += line;
            text += "\n";
        }
        string[] columns = text.Split("$%");
        Debug.Log("check child here");
        Debug.Log(columns[23]); 
        Debug.Log(columns[46]); 
        Debug.Log(columns[69]);
        Debug.Log(columns[23*4 + 12]);
        for(int i = 0; i < columns.Length; i++){
            int rowNumber = (int)Mathf.Floor(i/24);
            int colNumber = i%24 + rowNumber;
            if(columns[i] == ""){
                    continue;
            }
            else{
                if(colNumber > 11 && colNumber < 24){
                    if(rowNumber == 0){ 
                        sectionList[colNumber - 12].transform.GetChild(0).GetComponent<TMP_Text>().text = columns[i];
                    }
                    else{
                        //Debug.Log(column);
                        Debug.Log(colNumber.ToString() +"," + rowNumber.ToString() + columns[i]);
                        sectionList[colNumber - 12].transform.GetChild(1).GetChild(rowNumber-1).GetComponent<TMP_Text>().text = columns[i];
                    }
                }
            }
        }

    }

}