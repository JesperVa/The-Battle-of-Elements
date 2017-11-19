using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagerScript : MonoBehaviour
{
	//These values will be moved to inspector
	private const float MomentumIncreaseOnGround = 0.4f;
	private const float MomentumIncreaseInAir = 0.1f;
	private const float MaxMomentum = 0.5f;
    private const float MaxMomentumInAir = 0.3f;

    private const float ControllerOffset = 0.4f;
    
    private float m_timeSinceLastShot;

    [SerializeField]
    private float m_timeBetweenShots;

    [SerializeField]
    private bool m_facingRight;

	[SerializeField]
	private bool m_keyboard = true;

	private float m_momentum;

	private Vector2 m_inputs;
	private string m_thisControllerName;

	public Globals.PlayerNumber m_playerNumber;
    private int m_controllerCheck = 5;

	private PlayerScript m_playerScript;

	// Use this for initialization
	void Start () 
	{
        m_facingRight = true;
        m_playerScript = GetComponent<PlayerScript>();
        m_playerScript.SetDirection(Globals.Direction.Right);

        
        m_inputs = Vector2.zero;
		

		GetControllerType ();
        //Debug.Log("Start Player " + m_playerNumber);

        switch(m_playerNumber)
        {
            case Globals.PlayerNumber.One:
                m_controllerCheck = 1;
                break;
            case Globals.PlayerNumber.Two:
                m_controllerCheck = 2;
                break;
            case Globals.PlayerNumber.Three:
                m_controllerCheck = 3;
                break;
            case Globals.PlayerNumber.Four:
                m_controllerCheck = 4;
                break;
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
        m_timeSinceLastShot += Time.deltaTime;

        //m_characterAnimator.SetBool("shooting", true);

        HandleInput ();
		HandleDirection ();
		HandleMovement ();
        Flip();

        
        //m_characterYmovement = Input.GetAxis("Vertical");

          
    }

	void HandleInput ()
	{

        //if(m_playerNumber != Globals.PlayerNumber.One)
        //{
        //    m_keyboard = true;
        //}

        if (!m_keyboard)
		{
            if (m_controllerCheck <= Input.GetJoystickNames().Length) //Makes sure we don't get errors
            {
                m_inputs = Vector2.zero;
                m_inputs.x = Input.GetAxis("Horizontal" + m_thisControllerName + m_playerNumber.ToString());
                m_inputs.y = Input.GetAxis("Vertical" + m_thisControllerName + m_playerNumber.ToString());


                //Fix for issue with Jespers' controller
                //TODO: Remove
                if (m_inputs.x < 0.5 && m_inputs.x > -0.5)
                {
                    m_inputs.x = 0;
                }

                // if(Input.GetButtonDown("Fire3" + m_thisControllerName + m_playerNumber.ToString()))
                //{
                //  m_playerScript.PlayTauntVoice();
                //}

                if (Input.GetButtonDown("Jump" + m_thisControllerName + m_playerNumber.ToString()) && m_playerScript.IsOnGround())
                {
                    m_playerScript.Jump();
                }
                else if (Input.GetButtonDown("Jump" + m_thisControllerName + m_playerNumber.ToString()) && !m_playerScript.IsOnGround() && !m_playerScript.m_doubleJumped)
                {
                    m_playerScript.Jump();
                    m_playerScript.m_doubleJumped = true;
                }

                if (Input.GetButtonDown("Fire1" + m_thisControllerName + m_playerNumber.ToString()))
                {

                    if (m_timeBetweenShots < m_timeSinceLastShot)
                    {
                        m_timeSinceLastShot = 0;
                        m_playerScript.ShootCurrentElement();
                    }
                }

                if (Input.GetButtonDown("Fire2" + m_thisControllerName + m_playerNumber.ToString()))
                {
                    Debug.Log("Changed Element");
                    m_playerScript.ChangeElement();
                }
            }
		}
		else
		{
			//Debug.Log ("Keyboard test");
			m_inputs = Vector2.zero;
			//Honestly this is a pretty bad solution inside a unity script
			//It will only be here for the first preview to José
			if (Input.GetKey(KeyCode.A))
			{
				m_inputs.x = -1;
                //Debug.Log (m_inputs.x);
            }
			if (Input.GetKey(KeyCode.D))
			{
				m_inputs.x = 1;
            }

			if (Input.GetKeyDown(KeyCode.F))
			{
				m_playerScript.ShootCurrentElement();
			}
			if (Input.GetKeyDown(KeyCode.R))
			{
				m_playerScript.ChangeElement();
			}
			if (Input.GetKeyDown(KeyCode.Space) && m_playerScript.IsOnGround())
			{
				m_playerScript.Jump();
			}
			else if (Input.GetKeyDown(KeyCode.Space) && !m_playerScript.IsOnGround() && !m_playerScript.m_doubleJumped)
			{
				m_playerScript.Jump();
				m_playerScript.m_doubleJumped = true;
			}
		}
	}

    //TODO: Divide into 2 methods
	private void HandleDirection()
	{
        if (m_playerScript.IsOnGround())
        {
            if (m_inputs.x > 0 && m_momentum < MaxMomentum)
            {
                m_momentum += MomentumIncreaseOnGround;
                m_playerScript.SetDirection(Globals.Direction.Right);
            }
            if (m_inputs.x < 0 && m_momentum > (MaxMomentum * -1))
            {
                m_momentum -= MomentumIncreaseOnGround;
                m_playerScript.SetDirection(Globals.Direction.Left);
            }
            if (m_inputs.x == 0)
            {
                m_momentum = 0;
            }
            else if (m_momentum > MaxMomentum)
            {
                m_momentum = MaxMomentum;
            }
            else if (m_momentum < MaxMomentum * -1)
            {
                m_momentum = MaxMomentum * -1;
            }
        }
        else
        {
            if (m_inputs.x > 0 && m_momentum < MaxMomentumInAir)
            {
                m_momentum += MomentumIncreaseInAir;
                m_playerScript.SetDirection(Globals.Direction.Right);
            }
            if (m_inputs.x < 0 && m_momentum > (MaxMomentumInAir * -1))
            {
                m_momentum -= MomentumIncreaseInAir;
                m_playerScript.SetDirection(Globals.Direction.Left);
            }
            if (m_inputs.x == 0)
            {
                m_momentum = 0;
            }
            else if (m_inputs.x > 0 && m_momentum > MaxMomentumInAir)
            {
                m_momentum = MaxMomentumInAir;
            }
            else if (m_inputs.x < 0 && m_momentum < MaxMomentumInAir * -1)
            {
                m_momentum = MaxMomentumInAir * -1;
            }
        }
        

        if (m_inputs.y > ControllerOffset)
        {
            m_playerScript.SetDirection(Globals.Direction.Up);
        }
        if (m_inputs.y < -1 * ControllerOffset)
        {
            m_playerScript.SetDirection(Globals.Direction.Down);
        }


    }

	public void HandleMovement()
	{
        //Vector2 movement = new Vector2(m_inputs.x * m_playerScript.m_speed, m_inputs.y * m_playerScript.m_speed);
        //m_inputs.x = 1;
        //Vector2 movement = new Vector2(m_momentum * m_playerScript.m_speed, m_inputs.y * m_playerScript.m_speed);
        Vector2 movement = new Vector2(m_momentum * m_playerScript.m_speed, 0);
        //m_playerScript.transform.Translate(movement * Time.deltaTime);

        m_playerScript.Move(movement * Time.deltaTime);



       
    }

    private void Flip()
    {
        if (m_inputs.x > 0 && !m_facingRight || m_inputs.x < 0 && m_facingRight)
        {
            m_facingRight = !m_facingRight;

            Vector3 scale = transform.localScale;

            scale.x *= -1;

            transform.localScale = scale;

            //This makes sure movement doesn't feel floaty on keyboard
            m_momentum = 0;
        }
    }

	private void GetControllerType()
	{
		m_keyboard = false;
		string[] names = Input.GetJoystickNames();

		if (names.Length != 0)
		{
				//print(names[(int)m_playerNumber].Length);
			if (names [(int)m_playerNumber].Length == 19)
			{
				m_thisControllerName = "_ps_";
				print("PS4 CONTROLLER IS CONNECTED");
			}
			if (names [(int)m_playerNumber].Length == 33)
			{
				m_thisControllerName = "_xbox_";
				print("XBOX ONE CONTROLLER IS CONNECTED");
			}
			if (names [(int)m_playerNumber].Length == 0)
			{
				m_keyboard = true;
			}
		}
	}
}
