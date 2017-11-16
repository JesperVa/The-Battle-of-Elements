using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIKnockbackScript : MonoBehaviour {

	private Text m_playerKnockbackText;
	public PlayerScript m_player;
	// Use this for initialization
	void Awake () 
	{
		m_playerKnockbackText = GetComponent<Text> ();	
	}

	// Update is called once per frame
	void Update () 
	{
		m_playerKnockbackText.text = GameMasterScript.Instance.GetPlayerKnockback (m_player).ToString();
        // Om den andra lösningen blev för farlig kan vi pröva detta istället
        //m_playerKnockbackText.text = m_player.Knockback.ToString();
	}
}

