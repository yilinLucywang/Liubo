using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRaycast : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check for left click
        if(Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Left Click");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider != null)
                {
                    GameObject hitObject = hit.collider.gameObject;
                    //Check if hitObject has Clickable interface
                    Clickable myClickable = hitObject.GetComponent<Clickable>();
                    if(myClickable != null)
                    {
                        //If it is clickable, Invoke OnClick3D event
                        myClickable.CallOnClick3D();
                    }
                }
            }
        }
    }
}
