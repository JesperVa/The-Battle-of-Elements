using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : SingletonScript<SceneChanger> {

	public string m_LosingTeam;
    private List<PlayerData> m_playerdataList;

    [SerializeField]
    private List<PlayerScript> m_players;



	protected SceneChanger()
	{
		// guarantee this will be always, a singleton only - can't use the constructor!
	}

    void Awake()
    {
        m_playerdataList = new List<PlayerData>();
        //m_players = new List<PlayerScript>();
    }

    public void AddPlayer(PlayerData aPlayer)
    {
        m_playerdataList.Add(aPlayer);
    }

    public void RemovePlayer(PlayerData aPlayer)
    {
        m_playerdataList.Remove(aPlayer);
    }

    public void StartUpGame(string aScene)
    {
        foreach (PlayerScript player in m_players)
        {
            DontDestroyOnLoad(player.gameObject);
        }

        Scene tempScene = SceneManager.GetActiveScene();
        
        SceneManager.LoadScene(aScene, LoadSceneMode.Additive);

        //GameObject[] tempObjects = GameObject.FindGameObjectsWithTag("Spawnposition");
        List<PlayerScript> players = new List<PlayerScript>();

        for (int i = 0; i < m_playerdataList.Count; i++)
        {
            PlayerScript tempPlayer = Instantiate(m_players[i]) as PlayerScript;
            tempPlayer.GetComponent<InputManagerScript>().m_isActive = true;
            //tempPlayer.SetPosition(m_playerdataList[i].Position);
            tempPlayer.SetElements(m_playerdataList[i].ElementArray);
            //tempPlayer.SetTeam(m_playerdataList[i].Team);
            SceneManager.MoveGameObjectToScene(tempPlayer.gameObject, SceneManager.GetSceneByName(aScene));

            if(tempPlayer.GetComponent<InputManagerScript>().m_playerNumber == Globals.PlayerNumber.One)
            {
                tempPlayer.SetTeam(Globals.Team.Red);
            }
            if (tempPlayer.GetComponent<InputManagerScript>().m_playerNumber == Globals.PlayerNumber.Two)
            {
                tempPlayer.SetTeam(Globals.Team.Red);
            }
            if (tempPlayer.GetComponent<InputManagerScript>().m_playerNumber == Globals.PlayerNumber.Three)
            {
                tempPlayer.SetTeam(Globals.Team.Blue);
            }
            if (tempPlayer.GetComponent<InputManagerScript>().m_playerNumber == Globals.PlayerNumber.Four)
            {
                tempPlayer.SetTeam(Globals.Team.Blue);
            }


            players.Add(tempPlayer);
            //Debug.Log("Test" + i);
            
        }

        //Debug.Log(players.Count);


        //Destroys all the gameobjects in the older scene after the loadin has been finished
        foreach (GameObject go in tempScene.GetRootGameObjects())
        {
            Destroy(go);
        }

        //GameObject.Find("_GameMaster").GetComponent<GameMasterScript>().StartGame(players);
        //GameMasterScript.Instance.StartGame(players);

    }

	public void ChangeToWin(string aLosingTeam)
	{
		m_LosingTeam = aLosingTeam;

		SceneManager.LoadScene("WinScene");
	}
}
