using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKnockbackColor : MonoBehaviour {

    private Image m_playerKnockbackColor;
    private PlayerScript m_player;
    [SerializeField]
    private Globals.PlayerNumber number;
    public Text m_numberSize;
    public Color m_baseColor;
    // Use this for initialization
    void Awake()
    {

        m_player = GameObject.Find("Player" + number.ToString()).GetComponent<PlayerScript>();
        m_playerKnockbackColor = GetComponent<Image>();
        m_baseColor = m_playerKnockbackColor.color;
        m_numberSize.fontSize = 35;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player.m_takingDamage)
        {
            m_playerKnockbackColor.color = new Color32(255, 0, 0, 200);
            m_numberSize.fontSize = 45;
            Invoke("ChangeBackColor", 0.5f);
        }

        if (m_player.m_takingPowerUp)
        {
            m_playerKnockbackColor.color = new Color32(0, 255, 0, 200);
            m_numberSize.fontSize = 45;
            Invoke("ChangeBackColor", 0.5f);
        }


    }
    public void ChangeBackColor()
    {
        m_player.m_takingDamage = false;
        m_player.m_takingPowerUp = false;
        m_numberSize.fontSize = 35;
        m_playerKnockbackColor.color = m_baseColor;
    }
}
