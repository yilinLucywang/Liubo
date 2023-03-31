using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class StartUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [SerializeField] private TMP_InputField[] inputField;
    public GameData data;

    public void StartGame()
    {


        if (inputField[0].text == "")
            data.playername1 = "Player1";
        else
            data.playername1 = inputField[0].text;

        if (inputField[1].text == "")
            data.playername2 = "Player2";
        else
            data.playername2 = inputField[1].text;


        SceneManager.LoadScene("Liubo");
    }
}
