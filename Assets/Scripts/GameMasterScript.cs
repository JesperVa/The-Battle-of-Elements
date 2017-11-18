﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterScript : SingletonScript<GameMasterScript>
{
    // private SoundManagerScript m_soundManager;
    //  public string m_testSoundName;

    public ParticleSystem m_respawnParticles;

    [SerializeField]
    private List<PlayerScript> m_Players; //PlayerNumbers are based on Array number
    [SerializeField]
    private Transform[] m_DeathPositions; //Transform for object positions that indicate the death area
    [SerializeField]
    private Transform[] m_RespawnPositions;
    [SerializeField]
    private int m_RespawnTime;
    [SerializeField]
    private CameraControl m_Camera; 
    [SerializeField] 
    private int m_TeamLives;
    private Hashtable m_CurrentLives;
    private Queue<Transform> m_TransformQueue;

    private bool m_gameHasEnded = false;

    private string m_losingTeam = "";
    private int m_randRespawnValue;
    
    protected GameMasterScript()
    {
        // guarantee this will be always, a singleton only - can't use the constructor!
    }

    void Start()
    {

        #region Re-enable these when we're using sound
        //m_soundManager = SoundManagerScript.Instance;
        //m_soundManager.Play (m_testSoundName, true);
        #endregion


        m_TransformQueue = new Queue<Transform>();
        m_CurrentLives = new Hashtable();
        m_CurrentLives.Add(Globals.Team.Blue, m_TeamLives);
        m_CurrentLives.Add(Globals.Team.Red, m_TeamLives);

        //TODO:Makes sure that Players and Elements doesn't have collision
        //This will be removed and edited inside OnCollision2D for PlayerScript/ElementScript once development has gotten further
        //Physics2D.IgnoreLayerCollision (10, 9, true);
        //Ignore collision between Platforms and Elements
        //Physics2D.IgnoreLayerCollision(8, 9, true);


    }

    public void StartGame(List<PlayerScript> aPlayerList)
    {
        foreach (PlayerScript player in aPlayerList)
        {
            m_Camera.AddTarget(player.transform);
        }

        m_Players = aPlayerList;
    }

    void LateUpdate()
    {
	if (m_Players != null && m_Players.Count > 0)
        {
            CheckIfOutsideOfBounds();
            UpdateDeadPlayers();
        }
    }

    private void UpdateDeadPlayers()
    {
        foreach (PlayerScript player in m_Players)
        {
            if ((int)m_CurrentLives[player.GetTeam()] > 0 && player.isDead && player.deathTime > m_RespawnTime)
            {
                m_randRespawnValue = (int)(Random.value * m_RespawnPositions.Length - 1);
                if (!m_respawnParticles.isPlaying)
                {
                    m_respawnParticles.Play(m_RespawnPositions[m_randRespawnValue].transform);
                    m_Camera.AddTarget(m_RespawnPositions[m_randRespawnValue].transform);
                }

                //m_Camera.AddTarget(m_RespawnPositions[m_randRespawnValue].transform);

                Invoke("RespawnPlayer", 1.2f);
            }
        }
    }

    private void RespawnPlayer()
    {
        foreach (PlayerScript player in m_Players)
        {
            if ((int)m_CurrentLives[player.GetTeam()] > 0 && player.isDead && player.deathTime > m_RespawnTime)
            {
                player.transform.position = m_RespawnPositions[m_randRespawnValue].position;
                m_Camera.RemoveTarget(m_RespawnPositions[m_randRespawnValue].transform);
                m_Camera.AddTarget(player.transform);
                //Debug.Log(m_Camera.Targets());
                player.isDead = false;

            }
        }
    }

    public int GetTeamLives(PlayerScript aPlayer)
    {
        return (int)m_CurrentLives[aPlayer.GetTeam()];
    }

    public int GetPlayerKnockback(PlayerScript aPlayer)
    {
        return (int)aPlayer.Knockback;
    }

    public string GetLosingTeam()
    {
        return m_losingTeam;
    }

    public void EndGame()
    {
        if (!m_gameHasEnded)
        {
            m_gameHasEnded = true;
            Debug.Log("Game Over");
            //Logic for what happens when the game ends
            GameWon();
        }
    }

    private void GameWon()
    {
        SceneChanger.Instance.ChangeToWin(m_losingTeam);
        
    }

    private void CheckIfOutsideOfBounds()
    {
        //Checks if player is outside of death-area, if so remove from Camera transforms
        foreach (PlayerScript player in m_Players)
        {
            if (!player.isDead)
            {
                //This assumes the DeathPositons are put in the correct order inside this script
                //Correct order is: XRight, XLeft, YBottom
                Vector3 pPos = player.transform.position;
                if (pPos.x > m_DeathPositions[0].position.x || pPos.x < m_DeathPositions[1].position.x || pPos.y < m_DeathPositions[2].position.y)
                {
                    m_Camera.RemoveTarget(player.transform);
                    player.PlayKnockedOutVoice();
                    player.isDead = true;

                    //Gives errors if I try to do this in a cleaner way :(
                    int temp = (int)m_CurrentLives[player.GetTeam()];
                    temp -= 1;
                    m_CurrentLives[player.GetTeam()] = temp;
                    if ((int)m_CurrentLives[player.GetTeam()] <= 0)
                    {
                        Debug.Log(player.GetTeam() + " team is dead");
                        m_losingTeam = player.GetTeam().ToString();
                        EndGame();
			            break;
                    }
                }
            }
        }
    }
}
