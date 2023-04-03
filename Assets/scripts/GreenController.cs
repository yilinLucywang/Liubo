using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenController : MonoBehaviour
{
    public GameObject canvas;
    private GameState bd;
    [SerializeField] private GameObject hologramPrefab;
    private GameObject hologram;

    void Awake(){
        bd = canvas.GetComponent<GameState>();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnter()
    {
        bd.OnDestinationMouseEnter(transform.position);
        hologram = Instantiate(hologramPrefab, transform.position, Quaternion.identity);
    }

    public void OnMouseExit()
    {
        bd.OnDestinationMouseExit();
        Destroy(hologram);
    }

    private void OnDestroy()
    {
        if (hologram != null)
        {
            Destroy(hologram);
        }
    }

    public void OnClick(){
        //TODO: move current button to the position clicked
        Vector3 pos = gameObject.transform.position;
        //TODO: move piece
        Debug.Log("before move piece");
        bd.MovePiece(pos);
        Debug.Log("after move piece");
        //TODO: destroy all of the green spot
        Debug.Log("before remove");
        bd.RemoveGreens();
        Debug.Log("after remove");

        if (FindObjectOfType<GameState>().is_p1_turn && FindObjectOfType<GameState>().openLimit == true)
        {
            foreach (var btn in FindObjectOfType<GameState>().blackPieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = false;
            }
            foreach (var btn in FindObjectOfType<GameState>().whitePieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = false;
            }
        }
        else if(!FindObjectOfType<GameState>().is_p1_turn && FindObjectOfType<GameState>().openLimit == true)
        {
            foreach (var btn in FindObjectOfType<GameState>().blackPieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = false;
            }
            foreach (var btn in FindObjectOfType<GameState>().whitePieceBut.GetComponentsInChildren<Clickable>())
            {
                btn.interactable = false;
            }
        }
    }
}
