using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarController : MonoBehaviour
{
    
    private Rigidbody _rigidbody;
    private float preX = 0, preY = 0;

    public bool isMouseControlable;
    private bool isAngleLockActive = false;
    private HingeJoint hj;
    private IEnumerator lockControlCountdownCoroutine;

    public AudioSource ac;
    
    // [SerializeField] private Rigidbody childRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        ac = GetComponent<AudioSource>();
        isMouseControlable = true;
        hj = GetComponent<HingeJoint>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!isMouseControlable) return;
        if (hj.angle > 13)
        {
            isAngleLockActive = false;
            ac.Stop();
        }
        if (isAngleLockActive) return;
        var cons = 1f;
        
        var newX = Input.GetAxis("Mouse X");
        var deltaX = newX - preX;
        var newY = Input.GetAxis("Mouse Y");
        var deltaY = newY - preY;
        
        //Debug.Log(deltaY);
        deltaY = Mathf.Clamp(deltaY, -5, 5);
        deltaX = Mathf.Clamp(deltaX, -5, 5);
        var delta = new Vector2(deltaX, deltaY);
        var deltaMagnitude = delta.magnitude;
        // if (Mathf.Abs(deltaY) >= 0.5)
        // {
        //     sticks.ForEach(stick =>
        //     {
        //         stick.GetComponent<Rigidbody>().useGravity = false;
        //     });
        // }
        // else
        // {
        //     sticks.ForEach(stick =>
        //     {
        //         stick.GetComponent<Rigidbody>().useGravity = true;
        //     });
        // }
        var temp = new Vector3(deltaX * cons, 0, -deltaY * cons);
        //_rigidbody.AddForce(temp * 0.005f, ForceMode.VelocityChange);
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     _rigidbody.AddTorque(new Vector3(0, 0,  100), ForceMode.VelocityChange);
        //     sticks.ForEach(stick =>
        //     {
        //         stick.GetComponent<Rigidbody>().useGravity = false;
        //     });
        //     StartCoroutine(SticksGravityCoroutine());
        // }
        _rigidbody.AddRelativeTorque(new Vector3(0, 0,  -deltaMagnitude * cons), ForceMode.VelocityChange);
       
        preX = newX;
        preY = newY;



        if (hj.angle < 5)
        {
            isAngleLockActive = true;
            ac.Play();
        }
    }

    

    

    
}
