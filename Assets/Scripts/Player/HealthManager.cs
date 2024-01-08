using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
     public float maxHealth;
    public float baseMaxHealth = 100;
    float currentHealth;
   public GameObject xpOrb;
    private void Start()
    {
        maxHealth = baseMaxHealth ;
        currentHealth = maxHealth;
     
    }
    public void IncreaseHealthflat(float value)
    {
        currentHealth += value ;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
           
    }
    public void DecreaseHealthflat(float value)
    {
        currentHealth -= value;
        if (currentHealth <=0 )
        {
            Death();
        }

    }
    public void IncreaseHealthPercent(float value)
    {
        currentHealth += value*maxHealth;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

    }
    public void DecreaseHealthPercent(float value)
    {
        currentHealth -= value*maxHealth;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
        }

    }
    public void ChangeMaxHealthMultiplier(float value)
    {
        maxHealth *= value;
        currentHealth *= value;
    }
    void Death()
    {
        if(gameObject.tag == "Player")
        {
            //DoDeathSequence
        }
        else
        {
            Instantiate(xpOrb, gameObject.transform.position,gameObject.transform.rotation);
            //Instantiate(gameObject);
            AddSouls();
            //enemy may end up pickjing up xp
            Destroy(gameObject);
        }
    }
    void AddSouls()
    {
        Debug.Log("adding souls");
        GameObject[] spells = WeaponManager.WeaponManagerInstance.magicInventory;
        Debug.Log(spells);
        foreach(GameObject spell in spells)
        {
            if (spell !=null)
            {
                MagicTypeScript magicType = spell.GetComponent<MagicTypeScript>();
                if (magicType.magicType == Magic.Necromancy)
                {
                    NecromancyController necromancy = spell.GetComponent<NecromancyController>();
                    necromancy.SoulsCollected++;
                }
            }
          
         
        }
    }
 
}
