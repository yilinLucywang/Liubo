using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextProvider
{
    public static TextProvider Instance;
    // Start is called before the first frame update
    public static TextProvider GetInstance()
    {
        if (Instance == null)
        {
            Instance = new TextProvider();
        }
        return Instance;
    }
    
    public TextProvider()
    {
        
    }

    public string GetText(string id)
    {
        return "";
    }
}
