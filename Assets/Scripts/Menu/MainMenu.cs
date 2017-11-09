using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    public Transform MainCanvas;
    public Transform OptionsCanvas;
    public Transform SelectLevelCanvas;
    public Transform SelectCharacterCanvas;
    public Button returnBtn;

    public EventSystem eventSystem;
    public GameObject selectedObject;

    SoundManagerScript m_soundManager;
    public string m_menuSongName;

    public string chosenLevel;

    private bool buttonSelected;

    void Awake()
    {
        OptionsCanvas.gameObject.SetActive(false);
        SelectLevelCanvas.gameObject.SetActive(false);
        SelectCharacterCanvas.gameObject.SetActive(false); 

	m_soundManager = SoundManagerScript.Instance;
	m_soundManager.Play (m_menuSongName, true);
    }



    public void OptionsOn()
    {
        OptionsCanvas.gameObject.SetActive(true);
        MainCanvas.gameObject.SetActive(false);
        SelectLevelCanvas.gameObject.SetActive(false);
        SelectCharacterCanvas.gameObject.SetActive(false);


    }

    public void ReturnMainOn()
    {
        MainCanvas.gameObject.SetActive(true);
        OptionsCanvas.gameObject.SetActive(false);
        SelectLevelCanvas.gameObject.SetActive(false);
        SelectCharacterCanvas.gameObject.SetActive(false);
    }

    public void CharacterSelectionOn()
    {
        SelectCharacterCanvas.gameObject.SetActive(true);
        MainCanvas.gameObject.SetActive(false);
        SelectLevelCanvas.gameObject.SetActive(false);
        OptionsCanvas.gameObject.SetActive(false);
 
    }

    public void LevelSelectOn()
    {
        chosenLevel = null;
        SelectLevelCanvas.gameObject.SetActive(true);
        MainCanvas.gameObject.SetActive(false);
        SelectCharacterCanvas.gameObject.SetActive(false); 
        
    }

    //Kanske borde göras smidigare att lägga in fler kartor till spelet än att göra på detta sättet.
    public void SelectLevel1()
    {
        chosenLevel = "DevScene";
    }

    public void SelectLevel2()
    {
        chosenLevel = "TestingLevelDesign";
    }

    public void SelectLevel3()
    {
        chosenLevel = "TestingSoundManagerScene";
    }

    public void PlayLevel()
    {
        SceneManager.LoadScene(chosenLevel);
        //Application.LoadLevel(chosenLevel);
    }
 
    //Anävndas när vi lagt in 
    public void ToggleSound()
    {
       
    }
    public void SetMaterVolume(float volume)
    {

    }
    public void SetMusicVolume(float volume)
    {

    }
    public void SetSFXVolume(float volume)
    {

    }
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
