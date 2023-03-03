using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clickable : MonoBehaviour
{
    //Shows a function-picking interface in the inspector
    public UnityEvent OnClick3D;

    public bool interactable = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CallOnClick3D()
    {
        if (interactable)
        {
            OnClick3D.Invoke();
        }
    }

    //Use this to test if CallOnClick3D is working
    public void TestFunction()
    {
        Debug.Log("Received a click!");
    }
}
