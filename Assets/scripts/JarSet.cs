using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JarSet : MonoBehaviour
{
    [SerializeField] private GameObject jar;
    [SerializeField] private List<GameObject> sticks;
    [SerializeField] private Animator jarAnimator;

    private List<StickTransform> originalStickTransforms = new List<StickTransform>();

    private Vector3 originalJarPos;

    private Vector3 originalJarRot;
    
    
    // Start is called before the first frame update
    void Start()
    {
        StickRoller.GetInstance().onStickRoll.AddListener(ThrowJar);
        sticks.ForEach(stick =>
        {
            var stickTransform = new StickTransform();
            stickTransform.position = stick.transform.position;
            stickTransform.rotation = stick.transform.eulerAngles;

            originalStickTransforms.Add(stickTransform);
        });
        originalJarPos = jar.transform.position;
        originalJarRot = jar.transform.eulerAngles;
        
        StickRoller.GetInstance().onStickRollerEnable.AddListener(ResetJarAndSticks);
        StickRoller.GetInstance().onStickRollerDisable.AddListener(DisableJarAndSticks);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        jar.transform.position = originalJarPos;
        jar.transform.eulerAngles = originalJarRot;
        jar.GetComponent<JarController>().isMouseControlable = false;
        // lock mouse control
        
        yield return new WaitForSeconds(1.2f);
        ResetJarAndSticks();
    }
    
    void ResetJarAndSticks()
    {
        for (int i = 0; i < sticks.Count; i++)
        {
            sticks[i].SetActive(true);
            sticks[i].transform.position = originalStickTransforms[i].position;
            sticks[i].transform.eulerAngles = originalStickTransforms[i].rotation;
        }
        jar.SetActive(true);
        jar.transform.position = originalJarPos;
        jar.transform.eulerAngles = originalJarRot;
        jar.GetComponent<JarController>().isMouseControlable = true;
    }
    
    void DisableJarAndSticks()
    {
        sticks.ForEach(stick =>
        {
            stick.SetActive(false);
            //Destroy(stick);
        });
        jar.SetActive(false);
        jar.transform.position = originalJarPos;
        jar.transform.eulerAngles = originalJarRot;
        jar.GetComponent<JarController>().isMouseControlable = false;
        
    }
}
