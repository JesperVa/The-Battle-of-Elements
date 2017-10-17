using UnityEngine.Audio;
using UnityEngine;
using System;

public class SoundManagerScript : SingletonScript<SoundManagerScript>
{
	public AudioMixerGroup m_mixerGroup;

	[SerializeField]
	private SoundScript[] m_sounds;

	protected SoundManagerScript()
	{
		// guarantee this will be always, a singleton only - can't use the constructor!
	}

	void Awake()
	{
        var test = Instance;
        Debug.Log("Test");
		foreach (SoundScript sound in m_sounds)
		{
			sound.m_source = gameObject.AddComponent<AudioSource>();

			sound.m_source.clip = sound.m_clip;
			sound.m_source.loop = sound.m_isLooping;

			sound.m_source.outputAudioMixerGroup = m_mixerGroup;
		}
	}

	public void Play(string aName, bool aPlaying)
	{
		SoundScript sound = FindSoundInList (aName);
		SetVolume (sound);
		SetPitch (sound);
		if (aPlaying)
		{
			sound.m_source.Play ();
		}
		else
		{
			sound.m_source.Stop();
		}
	}

	public SoundScript FindSoundInList(string aName)
	{
		SoundScript sound = Array.Find(m_sounds, item => item.m_name == aName);
		if (sound == null)
		{
			Debug.LogWarning("Sound: " + aName + " not found!");
			return null;
		}

		return sound;
	}

	public void SetVolume(SoundScript aSound)
	{
		aSound.m_source.volume = aSound.m_volume;
	}

	public void SetPitch(SoundScript aSound)
	{
		aSound.m_source.pitch = aSound.m_pitch;
	}
}
