using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private GameState gs;
    private AudioSource ac;
    [SerializeField] private AudioClip roll;
    [SerializeField] private AudioClip landing;
    [SerializeField] private AudioClip owl;
    
    // Start is called before the first frame update
    void Start()
    {
        ac = GetComponent<AudioSource>();
        StickRoller.GetInstance().onStickRoll.AddListener(PlayRollingSound);
        gs.OnPieceLand.AddListener(PlayLandingSound);
        gs.OnTurningIntoOwl.AddListener(PlayOwlSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayRollingSound(int _a, int _b)
    {
        ac.PlayOneShot(roll);
    }
    
    void PlayLandingSound()
    {
        ac.PlayOneShot(landing);
    }

    void PlayOwlSound()
    {
        ac.PlayOneShot(owl);
    }
}
