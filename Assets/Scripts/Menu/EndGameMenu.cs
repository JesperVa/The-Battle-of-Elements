using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour 
{
	public static bool m_newGame = false;
	// Use this for initialization
	void Awake () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	public void NewGame()
	{
		//Load the Scene where the players select character and so on here
		m_newGame = true;
		SceneManager.LoadScene ("MainMenu");
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene ("MainMenu");
	}
}
