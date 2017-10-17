using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class SoundScript 
{
	public string m_name;

	public AudioClip m_clip;

	[Range(0f, 1f)]
	public float m_volume = 0.75f;

	[Range(0.1f, 3f)]
	public float m_pitch = 1f;

	public bool m_isLooping;

	public AudioMixerGroup m_mixerGroup;

	[HideInInspector]
	public AudioSource m_source;
}
