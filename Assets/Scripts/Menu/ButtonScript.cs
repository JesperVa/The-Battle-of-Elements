using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{

    public Toggle m_toggle;
    public Image m_backgoundImage;
	// Use this for initialization
	void Start ()
    {
        
        //m_backgoundImage.color = new Color32(0, 0, 255, 200);

    }
	
    public void ChangeBackground()
        {
            m_backgoundImage.color = new Color32(90, 150, 70, 200);
        }
    
            
    
	// Update is called once per frame
	void Update ()
    {
        if (m_toggle.isOn)
            ChangeBackground();
        else
            m_backgoundImage.color = new Color32(167, 47, 47, 200);
    }
}
