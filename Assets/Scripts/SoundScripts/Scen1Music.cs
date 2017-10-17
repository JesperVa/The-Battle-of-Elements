using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scen1Music : MonoBehaviour
{
    public AudioSource m_intro;
    public AudioSource m_loop;
    public AudioSource m_voice;

    private bool startedLoop;

    // Use this for initialization
    void Start()
    {
        m_intro.Play();
    }
    void FixedUpdate()
    {
        if (!m_intro.isPlaying && !startedLoop)
        {
            m_loop.Play();
            Debug.Log("Done playing");
            startedLoop = true;
        }
    }
    
	// Update is called once per frame
	void Update ()
    {
        
	}
}
