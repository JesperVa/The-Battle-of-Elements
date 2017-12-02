using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour 
{
	private Camera m_mainCamera;

	float m_shakeAmount = 0;

	bool m_debugMode = false;

	void Awake ()
	{
		m_mainCamera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (m_debugMode)
		{
			ShakeCamera(0.1f, 0.1f);
		}
	}

	public void ShakeCamera(float aAmount, float aTime) 
	{
		m_shakeAmount = aAmount;
		InvokeRepeating ("BeginShake", 0, 0.01f);
		Invoke ("StopShake", aTime);
	}

	void BeginShake()
	{
		if (m_shakeAmount > 0) 
		{
			Vector3 cameraPosition = m_mainCamera.transform.position;

			float offsetX = Random.value * m_shakeAmount * 2 - m_shakeAmount;
			float offsetY = Random.value * m_shakeAmount * 2 - m_shakeAmount;

			cameraPosition.x += offsetX;
			cameraPosition.y += offsetY;

			m_mainCamera.transform.position = cameraPosition;
		}
	}

	void StopShake()
	{
		CancelInvoke ("BeginShake");
		m_mainCamera.transform.localPosition = new Vector3(0, 0, -10);
	}
}
