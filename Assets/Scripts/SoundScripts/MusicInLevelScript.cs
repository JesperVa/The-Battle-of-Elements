using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicInLevelScript : MonoBehaviour
{
    public AudioSource m_intro;
    public AudioSource m_loop;
    public AudioSource m_voice;

    private bool startedLoop;

    // Use this for initialization
    void Start()
    {
        m_intro.Play();
        Invoke("PlayBeginVoice", 4.5f);
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

    void PlayBeginVoice()
    {
        m_voice.Play();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
