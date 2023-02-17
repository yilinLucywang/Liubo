using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }
}
