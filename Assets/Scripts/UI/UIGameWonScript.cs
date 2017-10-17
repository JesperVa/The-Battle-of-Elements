using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Text))]
public class UIGameWonScript : MonoBehaviour 
{
	private Text m_teamText;
	public Canvas m_endGameCanvas;
	public Canvas m_winCanvas;

	// Use this for initialization
	void Awake () 
	{
		m_teamText = GetComponent<Text> ();
		m_endGameCanvas.enabled = false;
		Invoke ("SwitchToEndScreen", 3.5f);
	}
	
	// Update is called once per frame
	void Update () 
	{
		string tempText = SceneChanger.Instance.m_LosingTeam;
		if (tempText == "Red")
		{
			m_teamText.text = "BLUE";
		}
		else
		{
			m_teamText.text = "RED";
		}
	}

	private void SwitchToEndScreen()
	{
		m_winCanvas.enabled = false;
		m_endGameCanvas.enabled = true;
	}
}