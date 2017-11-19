using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UITeamLivesScript : MonoBehaviour {

	private Text m_teamLivesText;
    private PlayerScript m_player;
    [SerializeField]
    private Globals.PlayerNumber number;
    // Use this for initialization
    void Awake () 
	{
		m_teamLivesText = GetComponent<Text> ();
        m_player = GameObject.Find("Player" + number.ToString()).GetComponent<PlayerScript>();
    }
	
	// Update is called once per frame
	void Update () 
	{
		m_teamLivesText.text = GameMasterScript.Instance.GetTeamLives (m_player).ToString();	
	}
}
