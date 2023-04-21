using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TyperEffect : MonoBehaviour
{
    public float delay = 0.1f;
    public string loadingTxt;

    private string currentTxt = "";

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowText());
    }

    // Update is called once per frame
    IEnumerator ShowText()
    {
        for(int i = 0; i <= loadingTxt.Length; i++)
        {
            currentTxt= loadingTxt.Substring(0, i);
            this.GetComponent<TextMeshProUGUI>().text = currentTxt;
            yield return new WaitForSeconds(delay);
        }
    }
}
