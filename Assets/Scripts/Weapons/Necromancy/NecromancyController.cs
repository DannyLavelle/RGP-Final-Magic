using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecromancyController : MonoBehaviour
{
    //TODO create level list (lv5 spawn skeletons)   
    float timer;
    public float castcooldown = 1;
    public float speedMultiplier;
    float SoulsCollected;
    public int level = 0;
    

   
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
            float zombiesToSpawn = SoulsCollected / 3 + 1;

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
        }


    }
}
