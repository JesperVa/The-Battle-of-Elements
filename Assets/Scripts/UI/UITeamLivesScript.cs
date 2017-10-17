using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UITeamLivesScript : MonoBehaviour {

	private Text m_teamLivesText;
	public PlayerScript m_player;
	// Use this for initialization
	void Awake () 
	{
		m_teamLivesText = GetComponent<Text> ();	
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_teamLivesText.text = GameMasterScript.Instance.GetTeamLives (m_player).ToString();	
	}
}
