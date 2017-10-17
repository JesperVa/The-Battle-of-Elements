using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

	[SerializeField]
	private int m_health = 100;
	//Used for resetting HP
	private int m_startHealth;
	private bool m_isDead;

	// Use this for initialization
	void Start () 
	{
		m_startHealth = m_health;
	}

	public void TakeDamage(int aDamageValue)
	{
		m_health -= aDamageValue;

		if (m_health < 0 && !m_isDead)
		{
			EnterDeadState ();
		}
	}

	private void EnterDeadState()
	{
		m_isDead = true;
		//TODO: Add whatever happens when player dies here
	}
}
