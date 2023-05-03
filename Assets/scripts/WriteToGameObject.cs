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
    public TMP_FontAsset englishA;
    public TMP_FontAsset chineseA;
 
    // public Material englishMA;
    // public Material chineseMA;
    public List<GameObject> sectionList;
    public List<TMP_Text> titles;
    public GameData gameData;
    // Start is called before the first frame update
    void Awake()
    {
        if(gameData.isEN){
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
            for(int i = 0; i < columns.Length; i++){
                int rowNumber = (int)Mathf.Floor(i/24);
                int colNumber = i%24;
                string con = columns[i].Substring(1,columns[i].Length-2);
                if(con.Length <= 1 && rowNumber != 0){
                    Debug.Log(columns[i]);
                    continue;
                }
                else{
                    if(colNumber > 11 && colNumber < 24){
                        if(rowNumber == 0){ 
                            sectionList[colNumber - 12].transform.GetChild(0).GetComponent<TMP_Text>().text = columns[i];
                            sectionList[colNumber - 12].transform.GetChild(0).GetComponent<TMP_Text>().font = chineseA;
                            titles[colNumber - 12].text = columns[i]; 
                            titles[colNumber - 12].font = chineseA;
                        }
                        else{
                            //Debug.Log(column);
                            //Debug.Log(colNumber.ToString() +"," + rowNumber.ToString() + con);
                            sectionList[colNumber - 12].transform.GetChild(1).GetChild(rowNumber-1).GetComponent<TMP_Text>().text = columns[i];
                            sectionList[colNumber - 12].transform.GetChild(1).GetChild(rowNumber-1).GetComponent<TMP_Text>().font = chineseA;
                        }
                    }
                }
            }
        }

    }

}