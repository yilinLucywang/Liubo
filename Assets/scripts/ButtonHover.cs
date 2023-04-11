using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UnityEvent OnButtonHoverEnter;
    public UnityEvent OnButtonHoverExit;
    [SerializeField] private GameObject hoverImg;

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
        Debug.Log("Enter");
        if (interactable)
        {
            OnButtonHoverEnter.Invoke();
        }
        hoverImg.SetActive(true);
    }

    public void CallOnButtonHoverExit()
    {
        Debug.Log("Exit");
        if (interactable)
        {
            OnButtonHoverExit.Invoke();
        }
        hoverImg.SetActive(false);
    }
}
