using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnButtonHoverEnter;
    public UnityEvent OnButtonHoverExit;

    public bool interactable = true;
    public void OnPointerEnter(PointerEventData eventData)
    {
        CallOnButtonHoverEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CallOnButtonHoverExit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallOnButtonHoverEnter()
    {
        if(interactable)
        {
            OnButtonHoverEnter.Invoke();
        }
    }

    public void CallOnButtonHoverExit()
    {
        if (interactable)
        {
            OnButtonHoverExit.Invoke();
        }
    }
}
