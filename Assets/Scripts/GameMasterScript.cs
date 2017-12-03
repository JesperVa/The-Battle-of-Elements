using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMasterScript : SingletonScript<GameMasterScript>
{
    // private SoundManagerScript m_soundManager;
    //  public string m_testSoundName;

    bool m_countDownDone = false; //Use this when countDown is ready

    public ParticleSystem[] m_respawnParticles;

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
    private CameraShake m_cameraShake;
    [SerializeField]
    private float m_cameraShakeAmount = 0.3f;
    [SerializeField]
    private int m_TeamLives;

    //Used for initalisation
    [SerializeField]
    private UIKnockbackScript[] m_KnockBackFields;
    [SerializeField]
    private UITeamLivesScript[] m_TeamLivesFiels;
    [SerializeField]


    private Hashtable m_CurrentLives;
    private Queue<Transform> m_TransformQueue;

    private bool m_gameHasEnded = false;

    private string m_losingTeam = "";
    private int[] m_randRespawnValue;

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

        GameObject[] TempGOs = GameObject.FindGameObjectsWithTag("Player");
        m_Players.Clear();
        foreach (GameObject go in TempGOs)
        {
            m_Players.Add(go.GetComponent<PlayerScript>());
        }
        StartGame(m_Players);


    }

    public void StartGame(List<PlayerScript> aPlayerList)
    {
        m_randRespawnValue = new int[aPlayerList.Count];

        foreach (PlayerScript player in aPlayerList)
        {
            //m_Camera.AddTarget(player.transform);
            player.SetImagePanels(player.GetComponent<InputManagerScript>().m_playerNumber);
            player.deathTime = m_RespawnTime + 1;
            player.isDead = true;

            Debug.Log(player.deathTime);
        }

        RespawnPlayer();

        Datagatherer.Instance.StartRecording(m_Players);

        //Debug.Log("In here we have: " + aPlayerList.Count);

        //m_Players = aPlayerList;
        //Datagatherer.Instance.StartRecording(m_Players);
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
        for (int i = 0; i < m_Players.Count; i++)
        {

            //Debug.Log(((int)m_CurrentLives[player.GetTeam()] > 0) + " " + player.isDead + " " + (player.deathTime > m_RespawnTime));

            if ((int)m_CurrentLives[m_Players[i].GetTeam()] > 0 && m_Players[i].isDead && m_Players[i].deathTime > m_RespawnTime && !m_Players[i].isRespawning)
            {
                
                m_randRespawnValue[i] = (int)(Random.value * m_RespawnPositions.Length);
                for (int j = 0; j < m_randRespawnValue.Length; j++)
                {
                    if(j != i && m_randRespawnValue[i]  == m_randRespawnValue[j])
                    {
                        m_randRespawnValue[i] = (int)(Random.value * m_RespawnPositions.Length);
                        j = 0;
                    }
                }


                if (!m_respawnParticles[m_randRespawnValue[i]].isPlaying)
                {
                    m_respawnParticles[m_randRespawnValue[i]].Play(m_RespawnPositions[m_randRespawnValue[i]].transform);
                    //Debug.Log("How often does thi trigger? Dead player: " + i);
                    m_Players[i].isRespawning = true;
                    //m_Camera.AddTarget(m_RespawnPositions[m_randRespawnValue[i]].transform);
                }
                

                //m_Camera.AddTarget(m_RespawnPositions[m_randRespawnValue].transform);

                Invoke("RespawnPlayer", 1.2f);
            }
            
        }
    }

    private void RespawnPlayer()
    {
        for (int i = 0; i < m_Players.Count; i++)
        {
            if ((int)m_CurrentLives[m_Players[i].GetTeam()] > 0 && m_Players[i].isDead && m_Players[i].deathTime > m_RespawnTime)
            {
                m_Players[i].transform.position = m_RespawnPositions[m_randRespawnValue[i]].position;
                m_Camera.RemoveTarget(m_RespawnPositions[m_randRespawnValue[i]].transform);
                m_Camera.AddTarget(m_Players[i].transform);
                //Debug.Log(m_Camera.Targets());
                m_Players[i].isDead = false;
                m_Players[i].isRespawning = false;
                m_randRespawnValue[i] = int.MaxValue;
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
            Datagatherer.Instance.StopRecording();
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

                    //Moves the player far down to make sure he isn't seen
                    //Pretty bad fix tbh

                    player.SetPosition(m_DeathPositions[2].position * 10);
                    player.PlayKnockedOutVoice();
                    player.isDead = true;
                    //TODO: Look into why camerashake kills the code
                    m_cameraShake.ShakeCamera(m_cameraShakeAmount, 0.3f);
                    //Gives errors if I try to do this in a cleaner way :(
                    int temp = (int)m_CurrentLives[player.GetTeam()];
                    temp -= 1;
                    m_CurrentLives[player.GetTeam()] = temp;

                    Debug.Log("We decreased lifes");
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
