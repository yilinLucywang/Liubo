using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GreenController : MonoBehaviour
{
    public GameObject canvas;
    private GameState bd;

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

    public void OnClick(){
        //TODO: move current button to the position clicked
        Vector2 pos = gameObject.transform.position;
        //TODO: move piece
        bd.MovePiece(pos);
        //TODO: destroy all of the green spot
        bd.RemoveGreens();

        if (FindObjectOfType<GameState>().is_p1_turn)
        {
            foreach (var btn in FindObjectOfType<GameState>().blackPieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            foreach (var btn in FindObjectOfType<GameState>().whitePieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
        }
        else
        {
            foreach (var btn in FindObjectOfType<GameState>().blackPieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
            foreach (var btn in FindObjectOfType<GameState>().whitePieceBut.GetComponentsInChildren<Button>())
            {
                btn.interactable = false;
            }
        }
    }
}
