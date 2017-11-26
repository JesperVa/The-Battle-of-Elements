using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSMaster : MonoBehaviour {


    private const int MaxPlayersPerTeam = 2;
    private int[] playersPerTeam; 

    private int AmountOfPlayers;

    [SerializeField]
    private MainMenu mainMenuScript;

    [SerializeField]
    private List<CharacterSelect> m_Players;

    public string MapChoice;
    
    private int readyUps;

	
    void Start()
    {
        AmountOfPlayers = m_Players.Count;
        mainMenuScript = Component.FindObjectOfType<MainMenu>();

    }

	// Update is called once per frame
	void Update ()
    {
        
        readyUps = 0;
        playersPerTeam = new int[2];

        foreach (CharacterSelect player in m_Players)
        {
            if(player.m_isReady)
            {
                readyUps++;
                if (player.Team == Globals.Team.Red)
                {
                    playersPerTeam[0]++;
                }
                else
                {
                    playersPerTeam[1]++;
                }
            }
        }
        //Debug.Log(Mathf.Max(playersPerTeam) <= MaxPlayersPerTeam);

		if(readyUps == AmountOfPlayers && Mathf.Max(playersPerTeam) <= MaxPlayersPerTeam)
        {
            //mainMenuScript.PlayLevel();
            SceneChanger.Instance.StartUpGame(mainMenuScript.chosenLevel);
            hasLoaded = true;
            
        }

        //Destroy(this); //Makes sure this is gone afterwards
    }
}
