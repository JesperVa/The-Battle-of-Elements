using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausMenu : MonoBehaviour {

    public Transform PausCanvas;
    public Transform OptionsCanvas; 
	// 
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
	}

    public void Pause()
    {
        if (PausCanvas.gameObject.activeInHierarchy == false)
        {
            PausCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PausCanvas.gameObject.SetActive(false);
            Time.timeScale = 1; 
        }
    }

    public void OptionsOn()
    {
 
        PausCanvas.gameObject.SetActive(false);
        OptionsCanvas.gameObject.SetActive(true);
    }

    public void Return()
    {
        PausCanvas.gameObject.SetActive(true);
        OptionsCanvas.gameObject.SetActive(false);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("MainMenu"); 
        //Application.LoadLevel("MainMenu");
        Time.timeScale = 1;
    }
}
