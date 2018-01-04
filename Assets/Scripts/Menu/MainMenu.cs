using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Audio;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    public Transform MainCanvas;
    public Transform OptionsCanvas;
    public Transform HowToPlayCanvas;
    public Transform SelectLevelCanvas;
    public Transform SelectCharacterCanvas;
    public Button returnBtn;

    public EventSystem eventSystem;
    public GameObject selectedObject;

    public Slider[] volumeSliders;
    public  AudioMixer audioMixer;
    
    //SoundManagerScript m_soundManager;
    //public string m_menuSongName;

    public string chosenLevel;

    private bool buttonSelected;

    void Awake()
    {
		if (EndGameMenu.m_newGame) {
			LevelSelectOn ();
			EndGameMenu.m_newGame = false;
		} 
		else 
		{
			OptionsCanvas.gameObject.SetActive(false);
			SelectLevelCanvas.gameObject.SetActive(false);
			SelectCharacterCanvas.gameObject.SetActive(false);
            HowToPlayCanvas.gameObject.SetActive(false);
		}

      

	//m_soundManager = SoundManagerScript.Instance;
	//m_soundManager.Play (m_menuSongName, true);
    }



    public void OptionsOn()
    {
        OptionsCanvas.gameObject.SetActive(true);
        HowToPlayCanvas.gameObject.SetActive(false);
        MainCanvas.gameObject.SetActive(false);
        SelectLevelCanvas.gameObject.SetActive(false);
        SelectCharacterCanvas.gameObject.SetActive(false);


    }

    public void HowToPlayOn()
    {
        OptionsCanvas.gameObject.SetActive(false);
        HowToPlayCanvas.gameObject.SetActive(true);
        MainCanvas.gameObject.SetActive(false);
        SelectLevelCanvas.gameObject.SetActive(false);
        SelectCharacterCanvas.gameObject.SetActive(false);


    }

    public void ReturnMainOn()
    {
        MainCanvas.gameObject.SetActive(true);
        OptionsCanvas.gameObject.SetActive(false);
        HowToPlayCanvas.gameObject.SetActive(false);
        SelectLevelCanvas.gameObject.SetActive(false);
        SelectCharacterCanvas.gameObject.SetActive(false);
    }

    public void CharacterSelectionOn()
    {
        SelectCharacterCanvas.gameObject.SetActive(true);
        MainCanvas.gameObject.SetActive(false);
        SelectLevelCanvas.gameObject.SetActive(false);
        OptionsCanvas.gameObject.SetActive(false);
        HowToPlayCanvas.gameObject.SetActive(false);

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
        chosenLevel = "EarthScene";
    }

    public void SelectLevel2()
    {
        chosenLevel = "WindScene";
    }

    public void SelectLevel3()
    {
        chosenLevel = "FireScene";
    }

    public void SelectLevel4()
    {
        chosenLevel = "WaterScene";
    }

    public void PlayLevel()
    {
        //SceneChanger.Instance.StartUpGame(chosenLevel);
        //SceneManager.LoadScene(chosenLevel);
        //Application.LoadLevel(chosenLevel);
    }

    //Anävndas när vi lagt in 
    public void ToggleSound()
    {
       
    }
    public void SetMaterVolume(float masterVolume)
    {
        masterVolume = volumeSliders[0].value;
        audioMixer.SetFloat("MasterVolume", masterVolume);
    }
    public void SetMusicVolume(float musicVolume)
    {
        musicVolume = volumeSliders[1].value;
        audioMixer.SetFloat("MusicVolume", musicVolume);
    }
    public void SetSFXVolume(float SFXVolume)
    {
        SFXVolume = volumeSliders[2].value;
        audioMixer.SetFloat("SFXVolume", SFXVolume);
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
