using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : SingletonScript<SceneChanger> {

	public string m_LosingTeam;

	protected SceneChanger()
	{
		// guarantee this will be always, a singleton only - can't use the constructor!
	}

	public void ChangeToWin(string aLosingTeam)
	{
		m_LosingTeam = aLosingTeam;
        
		SceneManager.LoadScene("WinScene");
	}
}
