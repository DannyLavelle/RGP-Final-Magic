using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancyController : MonoBehaviour
{
    //TODO create level list (lv5 spawn skeletons)   
    float timer;
    public float castcooldown = 15;
    public float speedMultiplier;
    public float SoulsCollected;
    public int level = 0;
    public int maxLevel = 5;
    float ExtraSpawns = 1;
    float soulsPerZombie = 3;
    float soulsPerSkeleton = 4;
   bool summonSkeletons = false;
    public Transform castPoint; //the players aim arrow not the player, may need to rotate 180 degrees but i will check later;
    public GameObject Skeleton;
    public GameObject Zombie;
    // Start is called before the first frame update
    void Start()
    {
        speedMultiplier = PlayerStats.instance.castSpeedMultiplier;
        GameObject castPointObject = GameObject.FindWithTag("castPoint");
        castPoint = castPointObject.transform;
    }

    // Update is called once per frame
    void Update()
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
        if (SoulsCollected > 0)
        {
            float zombiesToSpawn = (SoulsCollected / soulsPerZombie) + ExtraSpawns;

            // Loop to spawn zombies
            for (int i = 0; i < zombiesToSpawn; i++)
            {
                // Calculate offset based on a circular pattern
                float angle = (float)i / zombiesToSpawn * 2 * Mathf.PI;
                float offsetX = 2 * Mathf.Cos(angle);
                float offsetY = 2 * Mathf.Sin(angle);

                // Calculate spawn position
                Vector3 spawnPosition = transform.position + new Vector3(offsetX, offsetY, 0f);

                // Instantiate the zombie at the calculated position
                GameObject spawnedZombie = Instantiate(Zombie, spawnPosition, transform.rotation);
            }
            if(summonSkeletons)
            {
                float skeletonsToSpawn = (SoulsCollected / soulsPerSkeleton) + ExtraSpawns;

                // Loop to spawn skeletons
                for (int i = 0; i < skeletonsToSpawn; i++)
                {
                    // Calculate offset based on a circular pattern
                    float angle = (float)i / skeletonsToSpawn * 2 * Mathf.PI;
                    float offsetX = 2 * Mathf.Cos(angle);
                    float offsetY = 2 * Mathf.Sin(angle);

                    // Calculate spawn position
                    Vector3 spawnPosition = transform.position + new Vector3(offsetX, offsetY, 0f);

                    // Instantiate the skeleton at the calculated position
                    GameObject spawnedskeleton = Instantiate(Skeleton, spawnPosition, transform.rotation);
                }
            }
        }

        SoulsCollected = 0;
    }
    public string GetNextLevelDescription()
    {
       switch(level+1)
        {
            case 2:
            return ("Summon 1 extra zombie regardless of number of souls collected ");
            break;
            case 3:
            return ("reduce cast time by 1 second (Scales with cast Cooldown)");
            break ;
            case 4:
            return (" reduce the number of souls per zombie from 3 to 2");
            break;
            case 5:
            return ("can now Summon skeletons as well, what fire arrows from range");
                break;
            default:
            return ("Level List not working in Necromancy");
                break;
        }
    }
    public void IncreaseLevel()
    {
        level++;
        switch (level )
        {
            case 2:
            ExtraSpawns++;
            break;
            case 3:
            castcooldown--;
            break;
            case 4:
            soulsPerZombie --;
            break;
            case 5:
            summonSkeletons = true;
            break;
            default:
            Debug.Log("Level List not working in Necromancy");
            break;
        }
    }
}
