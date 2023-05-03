using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TyperEffect : MonoBehaviour
{
    public float delaySpeed;
    public string[] loadingTxtID;
    public string[] loadingTxtSourceID;
    public TextMeshProUGUI textBox;
    public TextMeshProUGUI sourceTextBox;

    private string currentTxt = "";

    // Start is called before the first frame update
    void Start()
    {
        StopCoroutine(ShowText());
        if (loadingTxtID.Length > 0)
        {
            StartCoroutine(ShowText());
        }
    }

    // Update is called once per frame
    IEnumerator ShowText()
    {
        var randIndex = Random.Range(0, loadingTxtID.Length);
        string randomTxtID = loadingTxtID[randIndex];
        string sourceTxtID = loadingTxtSourceID[randIndex];
         var randomTxt = TextProvider.Instance.GetText(randomTxtID);
         for (int j = 0; j <= randomTxt.Length; j++)
         {
             currentTxt = randomTxt.Substring(0, j);
             textBox.text = currentTxt;
             yield return new WaitForSeconds(delaySpeed);
         }

         currentTxt = "";
         var randomTxtSource = TextProvider.Instance.GetText(sourceTxtID);
         for (int j = 0; j <= randomTxtSource.Length; j++)
         {
             currentTxt = randomTxtSource.Substring(0, j);
             sourceTextBox.text = currentTxt;
             yield return new WaitForSeconds(delaySpeed);
         }
         //return null;
         yield return new WaitForSeconds(2f);
    }
}
