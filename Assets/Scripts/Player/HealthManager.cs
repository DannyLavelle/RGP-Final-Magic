using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
     public float maxHealth;
    public float baseMaxHealth = 100;
    public float currentHealth;
   public GameObject xpOrb;
    public Slider slider;
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
        UpdateSlider(); 
    }
    public void DecreaseHealthflat(float value)
    {
        currentHealth -= value;
        if (currentHealth <=0 )
        {
            Death();
        }
        UpdateSlider();
    }
    public void IncreaseHealthPercent(float value)
    {
        currentHealth += value*maxHealth;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UpdateSlider();
    }
    public void DecreaseHealthPercent(float value)
    {
        currentHealth -= value*maxHealth;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
        }
        UpdateSlider();
    }
    public void ChangeMaxHealthMultiplier(float value)
    {
        float tempMax = maxHealth;
        maxHealth = baseMaxHealth * value;
        currentHealth += (tempMax - maxHealth);
        UpdateSlider();
    }
    void Death()
    {
        if(gameObject.tag == "Player")
        {
            PlayerStats.instance.DeathSequence();
        }
        else
        {
            Instantiate(xpOrb, gameObject.transform.position,gameObject.transform.rotation);
            SpawnManager.recordedDeaths++;
            //Instantiate(gameObject);
            AddSouls();
            //enemy may end up pickjing up xp
            Destroy(gameObject);
        }
    }
    void AddSouls()
    {
        ;
        //GameObject[] spells = WeaponManager.WeaponManagerInstance.magicInventory;
        List<GameObject> spells = WeaponManager.WeaponManagerInstance.magicInventory;
        //Debug.Log(spells);
        foreach(GameObject spell in spells)
        {
            if (spell !=null)
            {
                MagicTypeScript magicType = spell.GetComponent<MagicTypeScript>();
                if (magicType.magicType == Magic.Necromancy)
                {
                    Debug.Log("adding souls");
                    NecromancyController necromancy = spell.GetComponent<NecromancyController>();
                    necromancy.SoulsCollected++;
                }
            }
          
         
        }
    }
    private void UpdateSlider()
    {
        if(gameObject.tag =="Player")
        {
            slider.value = currentHealth / maxHealth;
        }
       
    }
}
