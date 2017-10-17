using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticElement : Element
{
    [SerializeField]
    private int Duration;

    private float m_ActiveTime;

	
	void Start ()
    {
        m_ActiveTime = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_ActiveTime += Time.deltaTime;

        //Once the static element has been there for it's duration it is destroyed
        if(m_ActiveTime > Duration)
        {
            Destroy(gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D aCollider)
    {
        if(aCollider.gameObject.tag == "Player")
        {
            TryDealDamage(aCollider.GetComponent<PlayerScript>());
        }
    }
}
