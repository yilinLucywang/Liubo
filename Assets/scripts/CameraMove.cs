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
            if (Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.RightArrow))
            {
                transform.position = focusPoint.transform.position + Quaternion.Euler(0, _speed * -1 * Time.deltaTime, 0) * _offset;
            } else if (Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow))
            {
                transform.position = focusPoint.transform.position + Quaternion.Euler(0, _speed * Time.deltaTime, 0) * _offset;
            }
            //Just for recording, plz delete
            if (Input.GetKey(KeyCode.UpArrow))
            {
            transform.position = focusPoint.transform.position + Quaternion.Euler(_speed * -1 * Time.deltaTime, 0, 0) * _offset;
            } else if (Input.GetKey(KeyCode.DownArrow))
            {
            transform.position = focusPoint.transform.position + Quaternion.Euler(_speed * Time.deltaTime, 0, 0) * _offset;
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
            transform.rotation = Quaternion.LookRotation(focusPoint.transform.position - transform.position,Vector3.up);
    }
}
