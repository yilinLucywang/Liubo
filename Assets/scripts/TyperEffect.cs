using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TyperEffect : MonoBehaviour
{
    public float delay = 0.1f;
    public string[] loadingTxt;

    private string currentTxt = "";

    // Start is called before the first frame update
    void Start()
    {
        StopCoroutine(ShowText());
        if (loadingTxt.Length > 0)
        {
            StartCoroutine(ShowText());
        }
    }

    // Update is called once per frame
    IEnumerator ShowText()
    {
         string randomTxt = loadingTxt[Random.Range(0, loadingTxt.Length)];
         for (int j = 0; j <= randomTxt.Length; j++)
         {
             currentTxt = randomTxt.Substring(0, j);
             this.GetComponent<TextMeshProUGUI>().text = currentTxt;
             yield return new WaitForSeconds(delay);
         }
        
    }
}
