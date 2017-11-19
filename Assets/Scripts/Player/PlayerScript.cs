using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    #region Properties
    public bool isDead
    {
        get
        {
            return m_isDead;
        }
        set
        {
            if (value == true)
            {
                deathTime = 0; //Resets deathtimer if isDead is set to true
            }
            m_isDead = value;
        }
    }
    public float deathTime
    {
        get;
        private set;
    }

	public float Knockback
	{
		get
		{ 
			return m_increasingKnockbackValue;
		}
		private set
		{
			m_increasingKnockbackValue = value;
		}
	}
    #endregion

    private const int KnockBackOffset = 10;
    private const int MaxAvaliableSpells = 2;

    public Transform[] m_groundPoints;

    public AudioSource m_getHitVoice;
    public AudioSource m_knockedOutVoice;
    public AudioSource m_looserVoice;
    public AudioSource[] m_tauntVoice;

    public ParticleSystem m_healthPickupEffect;
    public ParticleSystem m_wandEffect;
    #region Serialized variables
    [SerializeField]
    private SpriteRenderer m_elementIndicator;
    [SerializeField]
    public int m_speed;
    [SerializeField]
    private int m_jumpSpeed;
    [SerializeField]
    private float m_groundRadius;
    [SerializeField]
    private LayerMask m_whatIsGround;
    [SerializeField]
    private ElementFactoryScript m_elementFactory;
    [SerializeField]
    private Globals.Team m_team;
    [SerializeField]
    private Transform m_shootingTransform;
    #endregion

    private bool m_isDead;
	private int m_spellIndex = 0;
    private Globals.Direction m_direction;
    private Rigidbody2D m_rigidbody;

    public bool m_doubleJumped = false;

    public bool m_takingDamage = false;

    public bool m_takingPowerUp = false;

    public float m_fallMultiplier = 3.5f;

    private Globals.Element m_currentElement;

    private Animator m_characterAnimator;
	private Globals.Element[] m_AvailableElements;

    [SerializeField]
    private const float m_minKnockbackValue = 5; //(K) constant knockback, set pretty low
    private float m_increasingKnockbackValue = 0; //X is a knockback value every player has that increases depending on their damage taken

    #region Monobehavior methods
    void Awake()
    {
        //Needed when players are handled inbetween scenes
        //DontDestroyOnLoad(this.gameObject);


        m_rigidbody = GetComponent<Rigidbody2D>();
        m_rigidbody.freezeRotation = true;
        m_currentElement = Globals.Element.Earth;

        m_characterAnimator = GetComponent<Animator>();
           
		m_AvailableElements = new Globals.Element[MaxAvaliableSpells];


        m_isDead = false;
        m_elementIndicator.color = Globals.BrownColor;

    }

    void Update()
    {
       
        m_characterAnimator.SetFloat("speedX", m_rigidbody.velocity.x);
        m_characterAnimator.SetFloat("speedY", m_rigidbody.velocity.y);


        if (IsOnGround())
            m_doubleJumped = false;

        if (m_isDead)
        {
            HandleDeath();
        }
        //The player falls faster on the way down from a jump
        if (m_rigidbody.velocity.y < 0)
        {
            m_rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (m_fallMultiplier - 1) * Time.deltaTime;
        }

		//Debug.Log (GetTeam());
        
    }
    #endregion

    #region Public Methods
    public Globals.Team GetTeam() { return m_team; }
	public void SetTeam(Globals.Team team){ m_team = team; }
    public void SetDirection(Globals.Direction aDirection) { m_direction = aDirection; }

    /// <summary>
    /// Movement method, can be called from both FixedUpdate() and Update()
    /// Make sure to add deltaTime to movement vector if called from Update()
    /// </summary>
    /// <param name="aMovement"></param>
    public void Move(Vector2 aMovement)
    {
        float speed = m_rigidbody.velocity.x;
        if (aMovement.x == 0 && IsOnGround() || aMovement.x > 0 && speed < 0 && IsOnGround() || aMovement.x < 0 && speed > 0 && IsOnGround())
        {
            //Makes sure if we're in a jump Y-speed isn't put to 0
            m_rigidbody.velocity = new Vector2(0, m_rigidbody.velocity.y);
        }
        else
        {
            m_rigidbody.velocity += new Vector2(aMovement.x, 0);
        }
    }

    //TODO: Add call for taunt in inputscript and bind buttons in inputmanager(Unity)
    public void PlayTauntVoice()
    {
        //int value = (int)(Random.value * m_tauntVoice.Length);
        int value = Random.Range(0, m_tauntVoice.Length);
        m_tauntVoice[value].Play();
    }


	public void SetElements(int aIndex, Globals.Element aElement)
	{
		m_AvailableElements [aIndex] = aElement;
		Debug.Log (m_AvailableElements [aIndex].ToString());
	}

    public void SetPosition(Vector2 aPostion)
    {
        Debug.Log("We tried to change position to: ");
        Transform tempTrans = GetComponent<Transform>();
        tempTrans.position = aPostion;
    }


    /// <summary>
    /// Throws System.Exception
    /// Used during loading for the prefab of chosen player
    /// </summary>
    /// <param name="anElementArray">An array containing the elements</param>
    public void SetElements(Globals.Element[] anElementArray)
    {
        if(anElementArray.Length > MaxAvaliableSpells)
        {
            throw new System.Exception("Too many spells in ElementArray");
        }
        else
        {
            Debug.Log("Inside playerscript: " + anElementArray[0] + " " + anElementArray[1]);
            m_AvailableElements = anElementArray;
        }
    }


    public void ShootCurrentElement()
    {
        //This can prob be done better
        BasicElementScript element = m_elementFactory.BasicEarth; //Gives errors if not set to a value
		//BasicElementScript element = m_AvailableElements[m_spellIndex];

        m_characterAnimator.SetTrigger("shooting");
        m_wandEffect.Play(m_wandEffect.transform);

        switch (m_currentElement)
        {
            case Globals.Element.Earth:
                element = m_elementFactory.BasicEarth;
                break;
            case Globals.Element.Fire:
                element = m_elementFactory.BasicFire;
                break;
            case Globals.Element.Wind:
                element = m_elementFactory.BasicWind;
                break;
            case Globals.Element.Water:
                element = m_elementFactory.BasicWater;
                break;
        }
        

        BasicElementScript tempGO = Instantiate(element, m_shootingTransform.position, new Quaternion()) as BasicElementScript;
        tempGO.SetOriginTeam(GetTeam());
        tempGO.SetOriginPlayer(this);

        if (m_direction == Globals.Direction.Right)
        {
            tempGO.SetDirection(Vector2.right);
        }
        else if (m_direction == Globals.Direction.Left)
        {
            tempGO.SetDirection(Vector2.left);
            tempGO.transform.localScale = new Vector3(tempGO.transform.localScale.x *-1, 
                                                    tempGO.transform.localScale.y, tempGO.transform.localScale.z);
        }
        else if(m_direction == Globals.Direction.Up)//Just to make sure it works from the get-go
        {
            tempGO.SetDirection(Vector2.up);
        }
        else if (m_direction == Globals.Direction.Down)//Just to make sure it works from the get-go
        {
            tempGO.SetDirection(Vector2.down);
        }
    }

    public void ChangeElement()
    {
        /*switch (m_currentElement)
        {
            case Globals.Element.Earth:
                m_currentElement = Globals.Element.Wind;
                m_elementIndicator.color = Color.white;
                break;
            case Globals.Element.Wind:
                m_currentElement = Globals.Element.Water;
                m_elementIndicator.color = Color.blue;
                break;
            case Globals.Element.Water:
                m_currentElement = Globals.Element.Fire;
                m_elementIndicator.color = Color.red;
                break;
            case Globals.Element.Fire:
                m_currentElement = Globals.Element.Earth;
                m_elementIndicator.color = Globals.BrownColor;
                break;
        }*/
		//if (m_spellIndex == 0) 
		//{
		//	m_spellIndex = 1;
		//	m_currentElement = m_AvailableElements [m_spellIndex];
		//} 
		//else 
		//{
		//	m_spellIndex = 0;
		//	m_currentElement = m_AvailableElements [m_spellIndex];
		//}

        m_spellIndex = ++m_spellIndex % MaxAvaliableSpells;
        m_currentElement = m_AvailableElements[m_spellIndex];
		Debug.Log (m_currentElement.ToString() + " " +  m_spellIndex);
    }

    public void Jump()
    {
        m_rigidbody.velocity += Vector2.up * m_jumpSpeed;
    }

    public bool IsOnGround()
    {
        //Either we're falling or on the ground
        if (m_rigidbody.velocity.y <= 0)
        {
            //Loops through all the points located on the bottom of the player
            foreach (Transform point in m_groundPoints)
            {
                //Add all objects colliding with the ground positions to an array
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, m_groundRadius, m_whatIsGround);
                foreach (Collider2D collider in colliders)
                {
                    //If the collider isn't the player we have collided with the ground
                    if (collider.gameObject != gameObject)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public void TakeDamage(float aDmgTaken, bool aKnockedFromRight)
    {
        m_takingDamage = true;
        m_characterAnimator.SetTrigger("knockbacked");
        m_getHitVoice.Play();  

	    Knockback += aDmgTaken;
        m_rigidbody.AddForce(transform.up * m_minKnockbackValue, ForceMode2D.Impulse);

        if (aKnockedFromRight)
        {
            m_rigidbody.AddForce(-transform.right * (m_minKnockbackValue + CalculateKnockBack()), ForceMode2D.Impulse);
        }
        else
        {
            m_rigidbody.AddForce(transform.right * (m_minKnockbackValue + CalculateKnockBack()), ForceMode2D.Impulse);
        }
       
    }

    public void PlayKnockedOutVoice()
    {
        m_knockedOutVoice.Play();
    }
    #endregion

    #region Private methods
    private void HandleDeath()
    {      
        deathTime += Time.deltaTime;
        m_rigidbody.velocity = Vector2.zero;
		Knockback = 0;
    }

    private void OnTriggerEnter2D(Collider2D aCollider)
    {
        if(aCollider.tag == "Health")
        {
            m_takingPowerUp = true;
            m_healthPickupEffect.Play(m_rigidbody.transform);
            if (m_increasingKnockbackValue < 10)
            {
                m_increasingKnockbackValue = 0;
            }
            else
            {
                m_increasingKnockbackValue -= 10;
            }
            Destroy(aCollider.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D aCollision)
    {
        if (aCollision.transform.tag == "MovingPlatform")
        {
            transform.parent = aCollision.transform;
        }
        

    }

    private void OnCollisionExit2D(Collision2D aCollision)
    {
        if (aCollision.transform.tag == "MovingPlatform")
        {
            transform.parent = aCollision.transform.parent.parent;
        }
    }


    private float CalculateKnockBack()
    {
        const float value = 1.05f; //(Y) constant value used to multiply the X value the larger it becomes
        Debug.Log(Knockback);
		return Mathf.Pow(Knockback/KnockBackOffset, value); // K+(X^Y)
    }
    #endregion
}
