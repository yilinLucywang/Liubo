using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class TextProvider : MonoBehaviour
{
    public static TextProvider Instance;
    public GameData gameData;
    private Dictionary<string, MultiLangText> texts = new Dictionary<string, MultiLangText>();
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        
        string filePath = @"Assets\scripts\UIText.txt";
        if(Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            filePath = @"Assets/scripts/UIText.txt";
        }
        char delimiter = ',';
        char quoteChar = '"';
        string lineBreak = "\r\n";
        if (Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer)
        {
            lineBreak = "\n";
        }
        
        string text = System.IO.File.ReadAllText(filePath);
        //var reg = "(?<=([^\"]*(\"[^\"]*\"[^\"]*)*[^\"]*))\r\n(?=([^\"]*(\"[^\"]*\"[^\"]*)*[^\"]*))";
        //string[] csvLines = Regex.Split(text, reg);
        bool inQuotes = false;
        string currentField = "";
        var result = new List<IList<string>>();
        var newLine = new List<string>();
        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            if (c == quoteChar)
            {
                // If we encounter a quote character, toggle the inQuotes flag
                inQuotes = !inQuotes;
                if (i > 0 && text[i - 1] == quoteChar)
                {
                    // If this quote is escaped, add it to the current field
                    currentField += c;
                }
            }
            else if (c == delimiter && !inQuotes)
            {
                // If we encounter a delimiter outside of quotes, we've finished a field
                newLine.Add(currentField);
                currentField = "";
            }
            else if (text.Substring(i).StartsWith(lineBreak) && !inQuotes)
            {
                // If we encounter a line break outside of quotes, we've finished a record
                newLine.Add(currentField);
                currentField = "";
                result.Add(newLine);
                newLine = new List<string>();
                i += lineBreak.Length - 1;
            }
            else
            {
                // Otherwise, just add the character to the current field
                currentField += c;
            }
        }
        // Handle the last field in the line, if any
        // if (!string.IsNullOrEmpty(currentField))
        // {
        newLine.Add(currentField);
        result.Add(newLine);
        // }

        for (int i = 1; i < result.Count; i++)
        {
            var line = result[i];
            texts.Add(line[0], new MultiLangText(line[0], line[1], line[2]));
        }
    }

    void Start()
    {
        
        
    }

    public string GetText(string id)
    {
        return texts[id].GetText(gameData.isEN ? Language.English : Language.Chinese);
    }
}

public struct MultiLangText
{
    public string id;
    private string en;
    private string cn;

    public MultiLangText(string id, string en, string cn)
    {
        this.id = id;
        this.en = en;
        this.cn = cn;
    }

    public string GetText(Language language)
    {
        switch (language)
        {
            case Language.Chinese:
                return cn;
            case Language.English:
                return en;
            default:
                return en;
        }
    }
}

public enum Language
{
    English,
    Chinese,
}
