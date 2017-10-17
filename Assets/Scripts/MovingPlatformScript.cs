using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformScript : MonoBehaviour 
{

	[SerializeField]
	private float m_moveSpeed;

	public GameObject m_platform;

	private Transform m_currentWaypoint;

	public Transform[] m_waypoints; //The way points which the platform will move to 

	private int m_waypointIndex = 1;

	private bool m_moveToStart = false;

	// Use this for initialization
	void Start () 
	{
		m_currentWaypoint = m_waypoints [m_waypointIndex];
	}

	void FixedUpdate () 
	{
		MovingPlatform ();
	}

	void MovingPlatform()
	{
		m_platform.transform.position = Vector3.MoveTowards (m_platform.transform.position, m_currentWaypoint.position, Time.deltaTime * m_moveSpeed);

		if (!m_moveToStart)
		{
			if (m_platform.transform.position == m_currentWaypoint.position && m_waypointIndex < m_waypoints.Length)
			{
				m_waypointIndex++;
				if (m_waypointIndex == m_waypoints.Length)
				{
					m_waypointIndex--;
					m_moveToStart = true;
				}
			}
		}
		else
		{
			if (m_platform.transform.position == m_currentWaypoint.position && m_waypointIndex > 0)
			{
				m_waypointIndex--;
				if (m_waypointIndex == 0)
				{
					m_moveToStart = false;
				}
			}
		}
		m_currentWaypoint = m_waypoints [m_waypointIndex];
	}
}
