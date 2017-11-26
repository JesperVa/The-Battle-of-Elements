using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour {

    [SerializeField]
    protected int m_knockbackDamage;

    protected Transform m_transform;

    protected Globals.Team m_originTeam;
    protected PlayerScript m_originPlayer;

    // Use this for initialization
    void Start ()
    {
        m_transform = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //When the object is EOS it will be destroyed
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    //TODO: This might be useless code, all child classes seems to need their own Collision function
    void OnTriggerEnter2D(Collider2D aCollider)
    {
        //if (aCollider.gameObject.tag == "BasicElement")
        //{
        //    Element collidingGO = aCollider.gameObject.GetComponent<Element>();
        //    StartCombination(collidingGO);
        //}

        if (aCollider.gameObject.tag == "Player")
        {
            TryDealDamage(aCollider.GetComponent<PlayerScript>());
        }
    }

    public void SetOriginPlayer(PlayerScript aPlayer)
    {
        m_originPlayer = aPlayer;
    }

    public void SetOriginTeam(Globals.Team aTeam)
    {
        m_originTeam = aTeam;
    }

    protected virtual void TryDealDamage(PlayerScript aTarget)
    {
        if (aTarget.GetTeam() != m_originTeam)
        {
            if (aTarget.transform.position.x < transform.position.x)
            {
                aTarget.TakeDamage(m_knockbackDamage, true); //the player got knocked from the right
            }
            else
            {
                aTarget.TakeDamage(m_knockbackDamage, false);
            }

            //When damage is dealt destroy the object
            //Destroy(gameObject);
        }
    }
}
