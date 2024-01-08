using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FireMagicController : MonoBehaviour
{
    public float timer;
    public float castcooldown = 1;
    public float speedMultiplier;
    
    public int level = 0;
    public GameObject basicFire;
    public GameObject flamethrower;
    public GameObject fireball;
    public GameObject combust;
    public int evolveLevel = 5;
    float projectileCount = 1;
    GameObject currentspell;
    public float damage = 10;
    public Transform castPoint;
    float flytime = 5;//the players aim arrow not the player, may need to rotate 180 degrees but i will check later;
    bool willPeirce = false;
    bool evolvedFireball;
    bool evolvedCombust;
    float maxOffset = .5f;
    private void Start()
    {
        speedMultiplier = PlayerStats.instance.castSpeedMultiplier;
        GameObject castPointObject = GameObject.FindWithTag("castPoint");
        castPoint = castPointObject.transform;
        
        //Debug.Log(castPoint);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        //Debug.Log(castcooldown * speedMultiplier);
        if (timer >= (castcooldown * speedMultiplier))
        {
            CastSpell();
           timer = 0;
        }
    }

    void CastSpell()
    {
        Vector2 SpawnPosition = castPoint.position ;
        GetCurrentSpell();
        
        //Debug.Log("Casting");
        for (int i = 0; i < projectileCount; i++)
        {
            GameObject spell;
            if (i!=0)
            {
                float offsetX = Random.Range(-maxOffset,maxOffset);
                float offsetY = Random.Range(-maxOffset, maxOffset);

                // Apply the offset to the rotation
              
                 SpawnPosition = castPoint.position + new Vector3(offsetX, offsetY);
                // Instantiate the spell with the rotated rotation
                spell = Instantiate(currentspell, SpawnPosition, castPoint.transform.rotation);
           
            }
            else
            {
                Debug.Log("combustion " + i + " (second) " + castcooldown);
                spell = Instantiate(currentspell, SpawnPosition, castPoint.rotation);
                
            }

            if (level < evolveLevel)
            {
                BasicFire fire = spell.GetComponent<BasicFire>();
                fire.GetStats(damage, willPeirce, flytime);
            }
            else 
            if (evolvedFireball)
            {
                currentspell = fireball;
                Fireball fireballs = spell.GetComponent<Fireball>();
                fireballs.GetStats(damage, willPeirce, flytime);
            }
            else if (evolvedCombust)
            {
               
                Combust combustion = spell.GetComponent<Combust>();
                combustion.GetStats(damage, willPeirce, flytime);
            }
            else
            {
                BasicFire fire = spell.GetComponent<BasicFire>();
                fire.GetStats(damage, willPeirce, flytime); //flamethrower is a bunch of basic fire 
            }
        }
      
       
    }
    void GetCurrentSpell()
    {
       if (level < evolveLevel)
        {
            currentspell = basicFire;
        }
       else if(evolvedFireball)
        {
            currentspell = fireball;
        }
       else if (evolvedCombust)
        {
            currentspell = combust;
        }
        else
        {
            currentspell = basicFire; //flamethrower is a bunch of basic fire 
        }
          
           
    }
    public string GetNextLevelDescription()
    {
        switch (level + 1)
        {
            case 2:
            return ("double damage");
            break;
            case 3:
            return ("fire can now peirce through enemies");
            break;
            case 4:
            return (" increase projectile by 4");
            break;
            case 5:
            return ("evolve");
            break;
            default:
            return ("Level List not working in Fire Magic");
            break;
        }
    }
    public void IncreaseLevel()
    {
        level++;
        switch (level)
        {
            case 2:
            damage *= 2;
            break;
            case 3:
            willPeirce = true;
            break;
            case 4:
            projectileCount+= 4;
            break;
            case 5:
            //evolve
            break;
            default:
            Debug.Log("Level List not working in Necromancy");
            break;
        }
    }
    public void EvolveWeapon(string Choice)
    {
        level++;
        switch (Choice)
        {
            case "Flamethrower"://dps 100
            projectileCount = 10;//SPWEW LOADS OF FLAMES
            damage = 15; //low damage per flame
            castcooldown /= 10;//spew flames quickly
            willPeirce = true;//make flames peirce
            flytime = .3f;//shortwen range to 1 second out
            maxOffset = 1;
                break;
            case"Fireball"://DPS 75 with medium AOE 
            projectileCount = 1;
            damage = 75;
            castcooldown = 2;
            evolvedFireball = true;
            willPeirce = false;
                break;
            case "Combust"://DPS 60 but massive AOE
            Debug.Log("Evolving COmbust");
            projectileCount = 1;
            damage = 300;
            castcooldown = 12;
            evolvedCombust = true;
            willPeirce = false;
            break;
        }
    }
}
 