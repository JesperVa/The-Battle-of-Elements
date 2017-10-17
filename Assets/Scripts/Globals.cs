using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Globals 
{
	public enum Element
	{
		Fire,
		Water,
		Earth,
		Wind
	}

    public enum Direction
    {
        Left,
        Right,
        Up,
        Down
    }

    public enum PlayerNumber
	{
		One,
		Two,
		Three,
		Four
	}

	public enum Team
	{
		Red,
		Blue
	}

	//This isn't inside Color for whatever reason
	public static Color BrownColor = new Color (0.50f, 0f, 0f); 
}
