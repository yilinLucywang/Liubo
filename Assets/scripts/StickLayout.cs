using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using ExtensionMethods;

public class StickTransform
{
    public string id;
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    public static StickTransform FromCsvLine(string line)
    {
        var ret = new StickTransform();
        var columns = Regex.Split(line, "[,]{1}(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))").Select(column => (column[0] == '"' && column[column.Length - 1] == '"') 
            ? column.Substring(1, column.Length - 2)
            : column).ToArray();
        //var columns = line.SplitWithQuote(',', '"');
        var posString = columns[1].Split(',');
        var rotString = columns[2].Split(',');
        var scaleString = columns[3].Split(',');
        ret.id = columns[0];
        ret.position = new Vector3(float.Parse(posString[0]), float.Parse(posString[1]), float.Parse(posString[2]));
        ret.rotation = new Vector3(float.Parse(rotString[0]), float.Parse(rotString[1]), float.Parse(rotString[2]));
        ret.scale = new Vector3(float.Parse(scaleString[0]), float.Parse(scaleString[1]), float.Parse(scaleString[2]));
        return ret;
    }
}

[Serializable]
public class StickLayout
{
    public List<String> stickIDs;

    public static StickLayout FromCsvLine(string line)
    {
        var ret = new StickLayout();
        var columns = Regex.Split(line, "[,]{1}(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))").Select(column => (column[0] == '"' && column[column.Length - 1] == '"') 
            ? column.Substring(1, column.Length - 2)
            : column).ToArray();
        ret.stickIDs = columns[1].Split(',').ToList();
        return ret;
    }
}

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static string[] SplitWithQuote(this string str, char separator, char quoteChar)
        {
            List<string> ret = new List<string>();
            bool open = false;
            int l = 0, r = 0;
            while (r < str.Length)
            {
                if (str[r] == quoteChar)
                {
                    open = !open;
                }
                if (str[r] == separator && !open)
                {
                    ret.Add(str.Substring(l, r-l));
                    l = r + 1;
                }
                r += 1;
            }
            ret.Add(str.Substring(l, r-l));
            return ret.ToArray();
        }
        
        public static string RemoveAll(this string s, char ch)
        {
            return new string(s.Where(c => c != ch).ToArray());
        }
    }
}


/*
{
    "layouts": [
        {
            "positions": [
            ],
            "rotations": [
            ],
            "scales": [
            ]
        },
        {
        }
    ]    
}

*/