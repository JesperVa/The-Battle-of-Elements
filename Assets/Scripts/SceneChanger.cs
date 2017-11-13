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

    public void addPlayer(PlayerData aPlayer)
    {
        m_playerdataList.Add(aPlayer);
    }

    public void StartUpGame(string aScene)
    {
        foreach (PlayerScript player in m_players)
        {
            DontDestroyOnLoad(player.gameObject);
        }

        Scene tempScene = SceneManager.GetActiveScene();
        GameObject tempObject = GameObject.FindGameObjectWithTag("Spawnposition");
        SceneManager.LoadScene(aScene, LoadSceneMode.Additive);

        for (int i = 0; i < m_playerdataList.Count; i++)
        {
            PlayerScript tempPlayer = Instantiate(m_players[i]) as PlayerScript;
            tempPlayer.SetPosition(m_playerdataList[i].Position);
            tempPlayer.SetElements(m_playerdataList[i].ElementArray);
            tempPlayer.SetTeam(m_playerdataList[i].Team);
            SceneManager.MoveGameObjectToScene(tempPlayer.gameObject, SceneManager.GetSceneByName(aScene));
        }

        //Debug.Log("Did we get here5");
        //SceneManager.GetSceneByBuildIndex(0);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));

        //Destroys all the gameobjects in the older scene after the loadin has been finished
        foreach (GameObject go in tempScene.GetRootGameObjects())
        {
            Destroy(go);
        }

        //SceneManager.SetActiveScene(tempScene);


        //Loads all the players into the scene
        //foreach (var player in m_playerdataList)
        //{
        //    Debug.Log("Just making sure inside scenechanger");
        //    PlayerScript tempPlayer = Instantiate(player) as PlayerScript;
        //    Debug.Log(tempPlayer.transform.position);
        //    tempPlayer.SetPosition(tempObject.transform);
        //    Debug.Log(tempPlayer.transform.position);
        //}



    }

	public void ChangeToWin(string aLosingTeam)
	{
		m_LosingTeam = aLosingTeam;
        
		SceneManager.LoadScene("WinScene");
	}
}
