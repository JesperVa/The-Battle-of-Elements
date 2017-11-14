using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKnockbackColor : MonoBehaviour {

    private Image m_playerKnockbackColor;
    public PlayerScript m_player;
    public Color m_baseColor;
    // Use this for initialization
    void Awake()
    {
        m_playerKnockbackColor = GetComponent<Image>();
        m_baseColor = m_playerKnockbackColor.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_player.m_takingDamage)
        {
            m_playerKnockbackColor.color = new Color32(255, 0, 0, 200);
            Invoke("ChangeBackColor", 0.5f);
        }
       

    }
    public void ChangeBackColor()
    {
        m_player.m_takingDamage = false;
        m_playerKnockbackColor.color = m_baseColor;
    }
}
