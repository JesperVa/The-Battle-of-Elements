using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicElementScript : MovingElement
{
    public enum ElementType
    {
        Fire,
        Water,
        Wind,
        Earth
    }

    public ElementType Type
    {
        get
        {
            return m_elementType;
        }
    }

    //This is a bad fix to a weird problem
    public bool hasCombined = false;

    [SerializeField]
    protected ElementType m_elementType;
    [SerializeField]
    protected ElementFactoryScript m_elementFactory;

    void OnTriggerEnter2D(Collider2D aCollider)
    {
        if (aCollider.gameObject.tag == "BasicElement")
        {
            BasicElementScript collidingGO = aCollider.gameObject.GetComponent<BasicElementScript>();
            StartCombination(collidingGO);
        }

        if (aCollider.gameObject.tag == "Player")
        {
            TryDealDamage(aCollider.GetComponent<PlayerScript>());
        }
    }

    protected override void TryDealDamage(PlayerScript aTarget)
    {
        base.TryDealDamage(aTarget);
        //When basicElement has hit target we destroy it
        //This will only happen if this is correct
        if (aTarget.GetTeam() != m_originTeam)
        {
            Destroy(gameObject);
        }
    }

    //This region contains all code used when combining basic elements
    #region Combination region

    private void StartCombination(BasicElementScript anElement)
    {
        if (anElement.Type != Type && m_originTeam == anElement.m_originTeam && hasCombined != true) //Don't combine if they are the same
        {
            switch (anElement.Type)
            {
                case ElementType.Fire:
                    CombineFire();
                    break;
                case ElementType.Water:
                    CombineWater();
                    break;
                case ElementType.Earth:
                    CombineEarth();
                    break;
                case ElementType.Wind:
                    CombineWind();
                    break;
            }

            //This needs to be set because the script still tries to run for the frame 
            //in the other GO even after being destroyed
            anElement.hasCombined = true;

            //Destroy the 2 colliding objects when combination is done
            Destroy(anElement.gameObject);
            Destroy(gameObject);
        }
    }

    private void CombineFire()
    {
        Debug.Log("CombineFire Entered");
        switch (Type)
        {
            case ElementType.Fire:
                //This won't happen
                break;
            case ElementType.Water:
                MovingElement WaterFire = Instantiate(m_elementFactory.WaterFireElement, m_transform.position, new Quaternion()) as MovingElement;
                WaterFire.SetDirection(m_direction);
                WaterFire.SetOriginTeam(m_originTeam);
                if (m_direction.x < 0)
                {
                    WaterFire.transform.localScale = new Vector3(WaterFire.transform.localScale.x * -1,
                                                    WaterFire.transform.localScale.y, WaterFire.transform.localScale.z);
                }
                break;
            case ElementType.Earth:
                StaticElement EarthFire = Instantiate(m_elementFactory.EarthFireElement, m_transform.position, new Quaternion()) as StaticElement;
                EarthFire.SetOriginTeam(m_originTeam);
                break;
            case ElementType.Wind:
                MovingElement WindFire = Instantiate(m_elementFactory.WindFireElement, m_transform.position, new Quaternion()) as MovingElement;
                WindFire.SetDirection(m_direction);
                WindFire.SetOriginTeam(m_originTeam);
                if (m_direction.x < 0)
                {
                    WindFire.transform.localScale = new Vector3(WindFire.transform.localScale.x * -1,
                                                    WindFire.transform.localScale.y, WindFire.transform.localScale.z);
                }
                break;
        }
    }

    private void CombineWater()
    {
        Debug.Log("CombineWater Entered");
        switch (Type)
        {
            case ElementType.Fire:
                MovingElement WaterFire = Instantiate(m_elementFactory.WaterFireElement, m_transform.position, new Quaternion()) as MovingElement;
                WaterFire.SetDirection(m_direction);
                WaterFire.SetOriginTeam(m_originTeam);
                if (m_direction.x < 0)
                {
                    WaterFire.transform.localScale = new Vector3(WaterFire.transform.localScale.x * -1,
                                                    WaterFire.transform.localScale.y, WaterFire.transform.localScale.z);
                }
                break;
            case ElementType.Water:
                //This won't happen
                break;
            case ElementType.Earth:
                StaticElement EarthWater = Instantiate(m_elementFactory.EarthWaterElement, m_transform.position, new Quaternion()) as StaticElement;
                EarthWater.SetOriginTeam(m_originTeam);
                break;
            case ElementType.Wind:
                StaticElement WindWater = Instantiate(m_elementFactory.WindWaterElement, m_transform.position, new Quaternion()) as StaticElement;
                WindWater.SetOriginTeam(m_originTeam);
                break;
        }

    }

    private void CombineWind()
    {
        switch (Type)
        {
            case ElementType.Fire:
                MovingElement WindFire = Instantiate(m_elementFactory.WindFireElement, m_transform.position, new Quaternion()) as MovingElement;
                WindFire.SetDirection(m_direction);
                WindFire.SetOriginTeam(m_originTeam);
                if (m_direction.x < 0)
                {
                    WindFire.transform.localScale = new Vector3(WindFire.transform.localScale.x * -1,
                                                    WindFire.transform.localScale.y, WindFire.transform.localScale.z);
                }
                break;
            case ElementType.Water:
                StaticElement WindWater = Instantiate(m_elementFactory.WindWaterElement, m_transform.position, new Quaternion()) as StaticElement;
                WindWater.SetOriginTeam(m_originTeam);
                break;
            case ElementType.Earth:
                MovingElement EarthWind = Instantiate(m_elementFactory.EarthWindElement, m_transform.position, new Quaternion()) as MovingElement;
                EarthWind.SetDirection(m_direction);
                EarthWind.SetOriginTeam(m_originTeam);
                if (m_direction.x < 0)
                {
                    EarthWind.transform.localScale = new Vector3(EarthWind.transform.localScale.x * -1,
                                                    EarthWind.transform.localScale.y, EarthWind.transform.localScale.z);
                }
                break;
            case ElementType.Wind:
                //Won't Happen
                break;
        }
    }

    private void CombineEarth()
    {
        switch (Type)
        {
            case ElementType.Fire:
                StaticElement EarthFire = Instantiate(m_elementFactory.EarthFireElement, m_transform.position, new Quaternion()) as StaticElement;
                EarthFire.SetOriginTeam(m_originTeam);
                break;
            case ElementType.Water:
                StaticElement EarthWater = Instantiate(m_elementFactory.EarthWaterElement, m_transform.position, new Quaternion()) as StaticElement;
                EarthWater.SetOriginTeam(m_originTeam);
                break;
            case ElementType.Earth:
                //Won't Happen
                break;
            case ElementType.Wind:
                MovingElement EarthWind = Instantiate(m_elementFactory.EarthWindElement, m_transform.position, new Quaternion()) as MovingElement;
                EarthWind.SetDirection(m_direction);
                EarthWind.SetOriginTeam(m_originTeam);
                break;
        }
    }
    #endregion

}
