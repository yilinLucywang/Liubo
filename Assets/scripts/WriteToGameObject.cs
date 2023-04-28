using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class WriteToGameObject : MonoBehaviour
{
    public List<GameObject> sectionList;
    // Start is called before the first frame update
    void Start()
    {
        string path = @"C:\Users\yilinwa2\Documents\GitHub\Liubo_2021\Assets\scripts\tutorialText.csv";
        string[] lines = System.IO.File.ReadAllLines(path);
        int rowCount = 0;
        foreach(string line in lines)
        {
            string[] columns = line.Split(',');
            int colCount = 0;
            foreach (string column in columns) {
                //sectionList[colCount].transform.GetChild(rowCount).text = column;
                Debug.Log(column);
                colCount += 1;
            }
            rowCount += 1;
        }
    }

}