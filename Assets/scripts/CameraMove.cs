using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float _speed;
    public GameObject focusPoint;
    public GameState GameState;

    private Vector3 _offset;
    private Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    private void Update()
    {
        _offset = transform.position - focusPoint.transform.position;
        
        //if ((GameState.state & State.MoveOrPieceSelection) != 0)
        //{
            if (Input.GetKey(KeyCode.E)) //|| Input.GetKey(KeyCode.RightArrow))
            {
                transform.position = focusPoint.transform.position + Quaternion.Euler(0, _speed * -1 * Time.deltaTime, 0) * _offset;
            } else if (Input.GetKey(KeyCode.Q)) //|| Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position = focusPoint.transform.position + Quaternion.Euler(0, _speed * Time.deltaTime, 0) * _offset;
            }
            //Just for recording, plz delete
            if (Input.GetKey(KeyCode.UpArrow))
            {
            transform.position = transform.position + new Vector3(0, 1 * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
            transform.position = transform.position + new Vector3(0, -1 * Time.deltaTime, 0);
        }
            if (Input.GetKey(KeyCode.RightArrow))
            {
            transform.position = new Vector3(transform.position.x + -1 * Time.deltaTime, transform.position.y , transform.position.z);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
            transform.position = new Vector3(transform.position.x + 1 * Time.deltaTime, transform.position.y, transform.position.z);
            }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = transform.position + new Vector3(0, 0, -1 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position + new Vector3(0, 0, 1 * Time.deltaTime);
        }
        //Until this line
        //}

        //if((GameState.state & State.MoveOrPieceSelection) == 0)
        //{
        //transform.position = initPos;
        //}
    }

    private void LateUpdate()
    {
        if(Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.Q))
            transform.rotation = Quaternion.LookRotation(focusPoint.transform.position - transform.position,Vector3.up);
    }
}
