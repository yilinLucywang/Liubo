using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class generateNumbers : MonoBehaviour
{

    public Text leftNum1;
    public Text leftNum2;  
    public Text rightNum1;
    public Text rightNum2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateLeftNumers(){
        int num1 = Random.Range(1, 5);
        int num2 = Random.Range(1, 5);
        leftNum1.text = "Left1: " + num1.ToString(); 
        leftNum2.text = "Left2: " + num2.ToString();
    }

    public void generateRightNumbers(){
        int num1 = Random.Range(1, 5);
        int num2 = Random.Range(1, 5);
        rightNum1.text = "Right1: " + num1.ToString(); 
        rightNum2.text = "Right2: " + num2.ToString();
    }
}
