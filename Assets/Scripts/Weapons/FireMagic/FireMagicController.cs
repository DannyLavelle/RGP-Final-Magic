using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMagicController : MonoBehaviour
{
    public float timer;
    public float castcooldown = 1;
    public float speedMultiplier = PlayerStats.castSpeedMultiplier;
    public int level = 0;
    public GameObject basicFire;
    public GameObject flamethrower;
    public GameObject fireball;
    public GameObject combust;
    public int evolveLevel = 5;
    GameObject currentspell;
    public Transform player; //the players aim arrow not the player, may need to rotate 180 degrees but i will check later;
    private void Update()
    {
        timer += Time.deltaTime;
        Debug.Log(castcooldown * speedMultiplier);
        if (timer >= (castcooldown * speedMultiplier))
        {
            CastSpell();
           timer = 0;
        }
    }

    void CastSpell()
    {
        Vector2 SpawnPosition = player.position ;
        GetCurrentSpell();
        Debug.Log("Casting");
        GameObject spell = Instantiate(currentspell, SpawnPosition, player.rotation);
       
    }
    void GetCurrentSpell()
    {
       if (level < evolveLevel)
        {
            currentspell = basicFire;
        }
    }
}
 