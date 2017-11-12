using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : SingletonScript<SceneChanger> {

	public string m_LosingTeam;
    private List<PlayerScript> m_players;


	protected SceneChanger()
	{
		// guarantee this will be always, a singleton only - can't use the constructor!
	}

    void Awake()
    {
        m_players = new List<PlayerScript>();
    }

    public void addPlayer(PlayerScript aPlayer)
    {
        m_players.Add(aPlayer);
    }

    public void StartUpGame(string aScene)
    {
        SceneManager.LoadScene(aScene);

        //Loads all the players into the scene
        foreach (var player in m_players)
        {
            PlayerScript tempPlayer = Instantiate(player) as PlayerScript;
        }

    }

	public void ChangeToWin(string aLosingTeam)
	{
		m_LosingTeam = aLosingTeam;
        
		SceneManager.LoadScene("WinScene");
	}
}
