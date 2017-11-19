using System;
using System.Collections.Generic;
using UnityEngine;

public class MovingElement : Element
{
    //Might only be needed if we're using a moving element?
    public uint m_speed;
    //Same as above
    protected Vector2 m_direction;

    void Update()
    {
        Movement();
    }

    public void SetDirection(Vector2 aDirection)
    {
        m_direction = aDirection;
        if(aDirection.x != 0)
        {
            m_speed += (uint)m_originPlayer.GetComponent<Rigidbody2D>().velocity.x;
        }
        else
        {
            m_speed += (uint)m_originPlayer.GetComponent<Rigidbody2D>().velocity.y;
        }
        
    }

    /// <summary>
    /// Default movement for basic elements
    /// Only moves towards set direction
    /// </summary>
    protected virtual void Movement()
    {
        m_transform.Translate(m_direction * m_speed * Time.deltaTime);
    }

}

