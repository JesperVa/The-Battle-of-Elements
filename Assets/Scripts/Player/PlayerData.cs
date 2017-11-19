using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{

    public Globals.Team Team
    {
        set;
        get;
    }

    public Globals.Element[] ElementArray
    {
        set;
        get;
    }

    public Vector2 Position
    {
        get;
        set;
    }


	public PlayerData()
    {

    }
}
