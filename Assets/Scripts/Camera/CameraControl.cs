using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// CameraControl zoomar in på spelarna i en array myTargets 
/// genom att använda/manipulera kamerans "size"
/// </summary>
public class CameraControl : MonoBehaviour
{
	public float m_dampTime = 0.2f;
	public float m_screenEdgeBuffer = 4f;
	public float m_minSize = 6.5f;

	//TODO: lägga till myMaxSize så att kameran aldrig visar något utanför grafiken

	//TODO: GameManager ska hålla variablerna senare
	/*[HideInInspector]*/ //public Transform[] m_targets;
	public List<Transform> m_targets;

	private Camera m_camera;
	private float m_zoomSpeed;
	private Vector3 m_moveVelocity;
	private Vector3 m_desiredPosition;

	private void Awake()
	{
        m_targets.Clear();
		m_camera = GetComponentInChildren<Camera>();
	}

	//TODO: ska uppdateras när spelarnas rörelse updateras, update?
	private void FixedUpdate()
	{
		Move();
		Zoom();
	}

	private void Move()
	{
		FindAveragePosition();
		transform.position = Vector3.SmoothDamp(transform.position, m_desiredPosition, ref m_moveVelocity, m_dampTime);
	}

	private void Zoom()
	{
		float requiredSize = FindRequiredSize();
		m_camera.orthographicSize = Mathf.SmoothDamp(m_camera.orthographicSize, requiredSize, ref m_zoomSpeed, m_dampTime);
	}

	private float FindRequiredSize()
	{
		Vector3 desiredLocalPos = transform.InverseTransformPoint(m_desiredPosition);

		float size = 0f;

		foreach (Transform target in m_targets)
		{
			if (!target.gameObject.activeSelf)
				continue;

			Vector3 targetLocalPos = transform.InverseTransformPoint(target.position);
			Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;
			size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));
			size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_camera.aspect);
		}

		size += m_screenEdgeBuffer;
		size = Mathf.Max(size, m_minSize);

		return size;
	}

	private void FindAveragePosition()
	{
		Vector3 averagePos = new Vector3();
		int numTargets = 0;

		foreach (Transform target in m_targets)
		{
			if (!target.gameObject.activeSelf)
				continue;

			averagePos += target.position;
			numTargets++;
		}

		if (numTargets > 0)
			averagePos /= numTargets;

		//Kanske ska vara z i detta fallet
		averagePos.y = transform.position.y;

		m_desiredPosition = averagePos;
	}

	//public för att kunna användas av en GameManager senare
	public void SetStartPosAndSize()
	{
		FindAveragePosition();
		transform.position = m_desiredPosition;
		m_camera.orthographicSize = FindRequiredSize();
	}

	public void RemoveTarget(Transform aTargetTransform)
	{
        Debug.Log("We tried to remove " + aTargetTransform.gameObject);
        Debug.Log(m_targets.Count);
		m_targets.Remove (aTargetTransform);
        Debug.Log("We did it");
        Debug.Log(m_targets.Count);
    }

	public void AddTarget(Transform aTargetTransform)
	{
		m_targets.Add (aTargetTransform);
	}

    public int Targets()
    {
        return m_targets.Count;
    }
}
