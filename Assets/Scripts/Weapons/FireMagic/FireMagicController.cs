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
    GameObject currentspell;
    public Transform castPoint; //the players aim arrow not the player, may need to rotate 180 degrees but i will check later;

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
        GameObject spell = Instantiate(currentspell, SpawnPosition, castPoint.rotation);
       
    }
    void GetCurrentSpell()
    {
       if (level < evolveLevel)
        {
            currentspell = basicFire;
        }
    }
}
 