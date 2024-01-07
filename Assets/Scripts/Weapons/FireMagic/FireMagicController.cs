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
    float damage = 10;
    public Transform castPoint;
    float flytime = 5;//the players aim arrow not the player, may need to rotate 180 degrees but i will check later;
    bool willPeirce = false;
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
                float randomOffset = Random.Range(-15f, 15f);

                // Apply the offset to the rotation
                Quaternion rotatedRotation = Quaternion.Euler(0, 0, castPoint.rotation.eulerAngles.z + randomOffset);

                // Instantiate the spell with the rotated rotation
                spell = Instantiate(currentspell, SpawnPosition, rotatedRotation);
            }
            else
            {
                spell = Instantiate(currentspell, SpawnPosition, castPoint.rotation);
            }

            if (level < evolveLevel)
            {
                BasicFire fire = spell.GetComponent<BasicFire>();
                fire.GetStats(damage, willPeirce,  flytime);
            }
        }
      
       
    }
    void GetCurrentSpell()
    {
       if (level < evolveLevel)
        {
            currentspell = basicFire;
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
}
 