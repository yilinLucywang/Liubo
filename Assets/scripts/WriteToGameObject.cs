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
        foreach(string line in lines)
        {
            string[] columns = line.Split(',');
            int colCount = 0;
            foreach (string column in columns) {
                if(colCount-12 >= 0 && colCount-12 < 13){
                    if(rowCount == 0){ 
                        //sectionList[colCount - 12].transform.GetChild(rowCount).GetComponent<TMP_Text>().text = column;
                    }
                    else{
                        //sectionList[colCount - 12].transform.GetChild(1).GetChild(rowCount - 1).GetComponent<TMP_Text>().text = column;
                    }
                }
                colCount += 1;
            }
            rowCount += 1;
        }
    }

}