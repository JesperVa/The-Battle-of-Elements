using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementFactoryScript : MonoBehaviour 
{
	[SerializeField]
	private BasicElementScript m_basicFire;
	[SerializeField]
	private BasicElementScript m_basicWater;
	[SerializeField]
	private BasicElementScript m_basicWind;
	[SerializeField]
	private BasicElementScript m_basicEarth;

	[SerializeField]
	private MovingElement m_waterFireElement;
    [SerializeField]
    private MovingElement m_earthWindElement;
    [SerializeField]
    private MovingElement m_windFireElement;

    [SerializeField]
    private StaticElement m_earthFireElement;
    [SerializeField]
    private StaticElement m_earthWaterElement;
    [SerializeField]
    private StaticElement m_windWaterElement;

    //private static Hashtable m_AdvancedElementsTable;

    #region Might be useful in the future
    /// <summary>
    /// Needs to be called before advanced elements are created
    /// This initalizes the HashTable the advanced elements are stored in
    /// </summary>
    //public static void Initalize()
    //{
    //    m_AdvancedElementsTable = new Hashtable(6);

    //    //TODO: Change what element is put on every key

    //    //Water+Fire
    //    int keyValue = m_basicFire.Type.GetHashCode() + m_basicWater.Type.GetHashCode();
    //    m_AdvancedElementsTable.Add(keyValue, m_waterFireElement);

    //    //Water+Wind
    //    keyValue = m_basicWind.Type.GetHashCode() + m_basicWater.Type.GetHashCode();
    //    m_AdvancedElementsTable.Add(keyValue, m_waterFireElement);

    //    //Water+Earth
    //    keyValue = m_basicEarth.Type.GetHashCode() + m_basicWater.Type.GetHashCode();
    //    m_AdvancedElementsTable.Add(keyValue, m_waterFireElement);

    //    //Fire+Wind
    //    keyValue = m_basicFire.Type.GetHashCode() + m_basicWind.Type.GetHashCode();
    //    m_AdvancedElementsTable.Add(keyValue, m_waterFireElement);

    //    //Fire+Earth
    //    keyValue = m_basicFire.Type.GetHashCode() + m_basicEarth.Type.GetHashCode();
    //    m_AdvancedElementsTable.Add(keyValue, m_waterFireElement);

    //    //Earth+Wind
    //    keyValue = m_basicWind.Type.GetHashCode() + m_basicEarth.Type.GetHashCode();
    //    m_AdvancedElementsTable.Add(keyValue, m_waterFireElement);


    //}

    //public static void SpawnAdvancedElement(BasicElementScript.ElementType aType1, BasicElementScript.ElementType aType2, Vector2 aSpawnPosition)
    //{
    //    int key = aType1.GetHashCode() + aType2.GetHashCode();
    //    Instantiate(m_AdvancedElementsTable[key], aSpawnPosition, new Quaternion()) as Element;
    //}
    #endregion

    #region Basic element properties
    public BasicElementScript BasicFire
	{
		get
		{
			return m_basicFire;
		}
	}

	public BasicElementScript BasicEarth
	{
		get
		{
			return m_basicEarth;
		}
	}

	public BasicElementScript BasicWind
	{
		get
		{
			return m_basicWind;
		}
	}

	public BasicElementScript BasicWater
	{
		get
		{
			return m_basicWater;
		}
	}

	public MovingElement WaterFireElement
	{
		get
		{
			return m_waterFireElement;
		}
	}
    #endregion

    #region Advanced element properties
    public MovingElement EarthWindElement
    {
        get
        {
            return m_earthWindElement;
        }
    }

    public StaticElement EarthFireElement
    {
        get
        {
            return m_earthFireElement;
        }
    }

    public StaticElement EarthWaterElement
    {
        get
        {
            return m_earthWaterElement;
        }
    }

    public MovingElement WindFireElement
    {
        get
        {
            return m_windFireElement;
        }
    }

    public StaticElement WindWaterElement
    {
        get
        {
            return m_windWaterElement;
        }
    }
    #endregion
}
