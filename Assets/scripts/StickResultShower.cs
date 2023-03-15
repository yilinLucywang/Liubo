using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ExtensionMethods;
using UnityEngine;

[System.Serializable]
public class Layout
{
    public List<Transform> transforms;
}

public class StickResultShower : MonoBehaviour
{
    [SerializeField] private List<GameObject> sticks;
    [SerializeField] private TextAsset stickTransformsFile;
    [SerializeField] private TextAsset layoutFile;

    [SerializeField] private Animator uiAnimator;
    //private List<StickTransform> stickTransforms;
    private Dictionary<string, StickTransform> stickTransforms;
    private List<StickLayout> layouts;

    private System.Random rand;
    // Start is called before the first frame update
    void Start()
    {
        rand = new System.Random();

        stickTransforms = stickTransformsFile.text.RemoveAll('\r').Split('\n').Skip(1)
            .Select(StickTransform.FromCsvLine)
            .ToDictionary(stickTransform => stickTransform.id, stickTransform => stickTransform);
        layouts = layoutFile.text.RemoveAll('\r').Split('\n').Skip(1).Select(StickLayout.FromCsvLine).ToList();

        StickRoller.GetInstance().onStickRoll.AddListener(ShowResult);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ShowResult(int val1, int val2)
    {
        // val is 0 ~ 7
        sticks.ForEach(stick => stick.SetActive(true));
        var stickTransformIDs = layouts[rand.Next(0, layouts.Count)].stickIDs;
        stickTransformIDs.Shuffle();
        
        var val = (val1 << 3) + val2;
        for (int i = 0; i < 6; i++)
        {
            sticks[i].transform.localPosition = stickTransforms[stickTransformIDs[i]].position;
            sticks[i].transform.localEulerAngles = stickTransforms[stickTransformIDs[i]].rotation;
            sticks[i].transform.localScale = stickTransforms[stickTransformIDs[i]].scale;
            if ((val & 1) == 1)
            {
                sticks[i].transform.Rotate(0, 180, 0, Space.Self);
            }
            val >>= 1;
        }
        uiAnimator.Play("ShowResult");
    }

    public void HideResult()
    {
        sticks.ForEach(stick => stick.SetActive(false));
    }
}

