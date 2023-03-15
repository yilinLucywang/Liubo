using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarController : MonoBehaviour
{
    
    private Rigidbody _rigidbody;
    private float preX = 0, preY = 0;
    [SerializeField] private Animator jarAnimator;

    [SerializeField] private List<GameObject> sticks;

    private List<StickTransform> originalStickTransforms = new List<StickTransform>();

    private Vector3 originalJarPos;

    private Vector3 originalJarRot;

    public bool isMouseControlable;
    // [SerializeField] private Rigidbody childRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        StickRoller.GetInstance().onStickRoll.AddListener(ThrowJar);
        sticks.ForEach(stick =>
        {
            var stickTransform = new StickTransform();
            stickTransform.position = stick.transform.position;
            stickTransform.rotation = stick.transform.eulerAngles;

            originalStickTransforms.Add(stickTransform);
        });
        originalJarPos = transform.position;
        originalJarRot = transform.eulerAngles;
        isMouseControlable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMouseControlable) return;
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
    }

    void ThrowJar(int _a, int _b)
    {
        jarAnimator.Play("Throw");
        StartCoroutine(SetSticksActive());
    }

    IEnumerator SetSticksActive()
    {
        yield return new WaitForSeconds(0.5f);
        sticks.ForEach(stick =>
        {
            stick.SetActive(false);
            //Destroy(stick);
        });
        transform.position = originalJarPos;
        transform.eulerAngles = originalJarRot;
        isMouseControlable = false;
        // lock mouse control
        
        yield return new WaitForSeconds(1.2f);
        for (int i = 0; i < sticks.Count; i++)
        {
            sticks[i].SetActive(true);
            sticks[i].transform.position = originalStickTransforms[i].position;
            sticks[i].transform.eulerAngles = originalStickTransforms[i].rotation;
        }

        transform.position = originalJarPos;
        transform.eulerAngles = originalJarRot;
        isMouseControlable = true;
    }
}
