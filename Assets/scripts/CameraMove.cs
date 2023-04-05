using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float _speed;
    public GameObject focusPoint;

    private Vector3 _offset;
    private Vector3 cameraPos;
    private float rotateAngle;

    private void Start()
    {
    }

    private void Update()
    {
        _offset = transform.position - focusPoint.transform.position;
        if (Input.GetKey(KeyCode.E))
            transform.position = focusPoint.transform.position + Quaternion.Euler(0, _speed * -1 * Time.deltaTime, 0) * _offset;
        if(Input.GetKey(KeyCode.Q))
            transform.position = focusPoint.transform.position + Quaternion.Euler(0, _speed * Time.deltaTime, 0) * _offset;
    }

    private void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(focusPoint.transform.position - transform.position,Vector3.up);
    }
}
