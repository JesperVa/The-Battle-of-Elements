using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Text;

public class Datagatherer : SingletonScript<Datagatherer> {

    private const float TimeBetweenUpdate = 0.8f;
    private float TimeSinceLastUpdate;

    [SerializeField] //Gotta check how the list is in the scene
    private List<PlayerScript> m_players;
    private List<Vector2>[] m_positions; //Array of lists storing positions
    private bool isRecordingData = false;
    private bool initialized = false;

	// Update is called once per frame
	void Update ()
    {
        if(initialized == false)
        {
            for (int i = 0; i < m_positions.Length; i++)
            {
                m_positions[i] = new List<Vector2>();
            }
            initialized = true;
        }
        TimeSinceLastUpdate += Time.deltaTime;
        Debug.Log(TimeSinceLastUpdate);
        if(isRecordingData && TimeSinceLastUpdate > TimeBetweenUpdate)
        {
            for (int i = 0; i < m_players.Count; i++)
            {
                if (!m_players[i].isDead) //Only record data if player is alive
                {
                    m_positions[i].Add(m_players[i].transform.position);
                }
            }
            TimeSinceLastUpdate = 0;
        }
	}

    /// <summary>
    /// Starts recording the Data
    /// </summary>
    /// <param name="aPlayerList">Players to record data from</param>
    public void StartRecording(List<PlayerScript> aPlayerList)
    {
        m_players = aPlayerList;
        isRecordingData = true;
        m_positions = new List<Vector2>[aPlayerList.Count];
        TimeSinceLastUpdate = TimeBetweenUpdate; //Starts recording immidietly
    }

    /// <summary>
    /// Stops recording the Data
    /// </summary>
    public void StopRecording()
    {
        isRecordingData = false;
        WriteGatheredData();
    }

    /// <summary>
    /// Serialises the data and writes it into a TXT file
    /// </summary>
    private void WriteGatheredData()
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\BattleOfElements";
        string fileName = @"\Game{0}.txt";

        System.IO.Directory.CreateDirectory(appDataPath); //Makes sure the appdata folder is there, if it is it doesn't do anything
        string[] count = System.IO.Directory.GetFiles(appDataPath);
        //Counts files in folder and names replay after that 
        fileName = string.Format(fileName, count.Length); 

        using (FileStream fs = File.Create(appDataPath + fileName))
        {
            AddText(fs, "Players:" + m_players.Count + Environment.NewLine);
            //Adds all the positions for the players, timeframes are seperated with ";"
            for (int i = 0; i < m_positions.Length; i++)
            {
                foreach (Vector2 position in m_positions[i])
                {
                    AddText(fs, position.ToString() + ";");
                }
                AddText(fs, Environment.NewLine);
            }
        }
    }

    /// <summary>
    /// Uses the filestream to add string value to the txt file
    /// </summary>
    /// <param name="fs">Filestream connected to the file</param>
    /// <param name="value">String value to be added</param>
    private void AddText(FileStream fs, string value)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(value);
        fs.Write(info, 0, info.Length);
    }
}
