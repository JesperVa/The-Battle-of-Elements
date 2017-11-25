using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIKnockbackScript : MonoBehaviour {

	private Text m_playerKnockbackText;
	private PlayerScript m_player;
    [SerializeField]
    private Globals.PlayerNumber number;
	// Use this for initialization
	void Awake () 
	{
		m_playerKnockbackText = GetComponent<Text> ();
        m_player = GameObject.Find("Player" + number.ToString() + "(Clone)").GetComponent<PlayerScript>();

        //If it wasn't found it's probably a Cloned object
        if (m_player == null)
        {
            m_player = GameObject.Find("Player" + number.ToString() + "(Clone)").GetComponent<PlayerScript>();
        }

        Debug.Log(m_player.gameObject.name);
    }

	// Update is called once per frame
	void Update () 
	{
		m_playerKnockbackText.text = GameMasterScript.Instance.GetPlayerKnockback (m_player).ToString();
        // Om den andra lösningen blev för farlig kan vi pröva detta istället
        //m_playerKnockbackText.text = m_player.Knockback.ToString();
	}
}

