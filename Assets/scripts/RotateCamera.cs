using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float _speed;
    public GameObject focusPoint;
    public float constraintVolume;

    private Vector3 _offset;
    private Vector3 positionForCamera;

    private void Start()
    {
        _offset = transform.position - focusPoint.transform.position;
        positionForCamera = transform.position;
        constraintVolume = Mathf.Abs(constraintVolume);
        Debug.Log(transform.rotation);
    }

    private void Update()
    {
        if (positionForCamera.z > -1 * constraintVolume)
        {
            if (Input.GetKey(KeyCode.D))
            {
                positionForCamera = new Vector3(transform.position.x,transform.position.y, transform.position.z + _speed * -1 * Time.deltaTime);
            }
        }
        if(positionForCamera.z < constraintVolume)
        {
            if (Input.GetKey(KeyCode.A))
            {
                positionForCamera = new Vector3(transform.position.x, transform.position.y, transform.position.z + _speed * Time.deltaTime);
            }
        }
        
    }

    private void LateUpdate()
    {
        transform.position = positionForCamera;
        transform.rotation = Quaternion.LookRotation(focusPoint.transform.position - transform.position, Vector3.up);
    }
}
