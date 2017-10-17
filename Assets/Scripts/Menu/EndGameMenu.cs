using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour 
{
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
	}

	public void GoToMainMenu()
	{
		SceneManager.LoadScene ("MainMenu");
	}
}
