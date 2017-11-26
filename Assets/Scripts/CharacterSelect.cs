using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharacterSelect : MonoBehaviour
{

    public List<string> m_SpellList;
    //public List<Sprite> m_SpriteList;

    private int m_SpellOneIndex;
    private int m_SpellTwoIndex = 1;

    private bool m_isReady = false;

    private Globals.Element[] m_ChosenElements;

    public Text m_SpellOneText;
    public Text m_SpellTwoText;
    public Text m_TeamText;

    //Instead of creating a seperate object that checks all the other objects 
    private static int AmountOfReadyUps = 0;

    private const int MaxPlayers = 4; //This needs to be removed and added non-hardcoded

    private bool m_TeamRed = true;
    //public PlayerScript m_player;

    private PlayerData m_playerdata;

    public ElementFactoryScript m_Element;
    //private BasicElementScript m_Element;
    // Use this for initialization
    void Awake()
    {
        m_playerdata = new PlayerData();
        TeamSelect();
        m_ChosenElements = new Globals.Element[2];
        FirstSpellSelect();
        SecondSpellSelect();
        //Makes sure nothing is lost in scene change
        DontDestroyOnLoad(this.gameObject);

        m_SpellOneText.text = m_SpellList[m_SpellOneIndex];
        m_SpellTwoText.text = m_SpellList[m_SpellTwoIndex];
        //m_Element = new BasicElementScript ();
        //m_Element = new ElementFactoryScript();

        
        
    }

    public void TeamSelect()
    {
        if (m_TeamText.text == "Red Team")
        {
            m_TeamRed = false;
            m_TeamText.text = "Blue Team";
            //m_player.SetTeam (Globals.Team.Blue);
            m_playerdata.Team = Globals.Team.Blue;

        }
        else
        {
            m_TeamRed = true;
            m_TeamText.text = "Red Team";
            //m_player.SetTeam(Globals.Team.Red);
            m_playerdata.Team = Globals.Team.Red;
        }
    }

    void SpriteSelect()
    {
        //if (team = red) {
        //		if(true){ sprite = sprite1}
        //		else{ sprite = sprite2}
        //}
        //if (team = blue) {
        //		if(true){ sprite = sprite3}
        //		else{ sprite = sprite4}
        //}
    }

    private void CheckElement(int aIndex, int aSpellIndex)
    {

        if (m_SpellList[aSpellIndex] == "Fire")
        {
            m_ChosenElements[aIndex] = Globals.Element.Fire;
        }
        else if (m_SpellList[aSpellIndex] == "Water")
        {
            m_ChosenElements[aIndex] = Globals.Element.Water;
        }
        else if (m_SpellList[aSpellIndex] == "Wind")
        {
            m_ChosenElements[aIndex] = Globals.Element.Wind;
        }
        else if (m_SpellList[aSpellIndex] == "Earth")
        {
            m_ChosenElements[aIndex] = Globals.Element.Earth;
        }

        #region Old code
        //if (m_SpellList [aSpellIndex] == "Fire") 
        //{
        //	m_player.SetElements (aIndex, Globals.Element.Fire);
        //}
        //else if (m_SpellList [aSpellIndex] == "Water") 
        //{
        //	m_player.SetElements (aIndex, Globals.Element.Water);
        //}
        //else if (m_SpellList [aSpellIndex] == "Wind") 
        //{
        //	m_player.SetElements (aIndex, Globals.Element.Wind);
        //}
        //else if (m_SpellList [aSpellIndex] == "Earth") 
        //{
        //	m_player.SetElements (aIndex, Globals.Element.Earth);
        //}
        #endregion
    }
    public void FirstSpellSelect()
    {
        m_SpellOneIndex++;

        if (m_SpellOneIndex >= m_SpellList.Count)
        {
            m_SpellOneIndex = 0;
        }
        m_SpellOneText.text = m_SpellList[m_SpellOneIndex];
        CheckElement(0, m_SpellOneIndex);
    }

    public void SecondSpellSelect()
    {
        m_SpellTwoIndex++;

        if (m_SpellTwoIndex >= m_SpellList.Count)
        {
            m_SpellTwoIndex = 0;
        }
        m_SpellTwoText.text = m_SpellList[m_SpellTwoIndex];
        CheckElement(1, m_SpellTwoIndex);

    }

    public void ReadyUp()
    {

        if (!m_isReady)
        {
            m_playerdata.ElementArray = m_ChosenElements;
            SceneChanger.Instance.AddPlayer(m_playerdata);
            AmountOfReadyUps++;
            m_isReady = true;

        }
        else
        {
            SceneChanger.Instance.RemovePlayer(m_playerdata);
            AmountOfReadyUps--;
            m_isReady = false;
        }

        //Debug.Log(AmountOfReadyUps);

        if (AmountOfReadyUps == MaxPlayers)
        {
            SceneChanger.Instance.StartUpGame("DevScene");
        }


        //DontDestroyOnLoad (m_player);
        //SceneManager.LoadScene ("DevScene");
    }
}
