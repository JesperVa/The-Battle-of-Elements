using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{


    public enum WaveForm { sin, tri, sqr, saw, inv, noise}
    public WaveForm m_waveForm = WaveForm.sin;

    public float m_baseStart = 0.0f;  //start
    public float m_amplitude = 1.0f;  //ampltude of the wave
    public float m_phase = 0.0f;      //start point inside on wave cycle
    public float m_frequency = 0.5f;  //cycle frequency per second

    //Keep a copy of the original color
    private Color m_originalColor;
    private Light m_light;


    // Use this for initialization
    void Start ()
    {
        m_light = GetComponent<Light>();
        m_originalColor = m_light.color;

		
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_light.color = m_originalColor * (EvalWave());
		
	}

    float EvalWave()
    {
        float x = (Time.time + m_phase) * m_frequency;
        float y;

        x = x - Mathf.Floor(x); // normalized value (0,1)

        if (m_waveForm == WaveForm.sin)
        {
            y = Mathf.Sin(x * 2 * Mathf.PI);
        }

        else if (m_waveForm == WaveForm.tri)
        {
            if (x < 0.5f)
                y = 4.0f * x - 1.0f;
            else
                y = -4.0f * x + 3.0f;

        }

        else if (m_waveForm == WaveForm.sqr)
        {
            if (x < 0.5f)
                y = 1.0f;
            else
                y = -1.0f;
        }

        else if (m_waveForm == WaveForm.saw)
        {
            y = x;
        }

        else if (m_waveForm == WaveForm.inv)
        {
            y = 1.0f - x;
        }

        else if (m_waveForm == WaveForm.noise)
        {
            y = 1f * (Random.value * 2);
        }

        else
        {
            y = 1.0f;
        }

        return (y * m_amplitude) + m_baseStart;
    }
}
