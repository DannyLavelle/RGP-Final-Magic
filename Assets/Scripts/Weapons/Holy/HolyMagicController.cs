using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyMagicController : MonoBehaviour
{
    float timer;
    public float castcooldown = 5;
    public float speedMultiplier;
    float SoulsCollected;
    public int level = 0;
    public float healAmountFlat = 10;
    float healAmountPercent = 0;
    public int maxLevel = 5;
    bool enableHealToDamage = false;
 
    // Start is called before the first frame update
    void Start()
    {
        speedMultiplier = PlayerStats.instance.castSpeedMultiplier;
     
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
        float healamount = healAmountFlat; // + playermaxhealth *;
        Debug.Log("Holy Magic Triggering");
      //healPlayer
      if(enableHealToDamage)
        {
            float damageMultiplier = (healamount * 0.1f)/100;
            PlayerStats.instance.holy5DamageMultiplier = 1 + damageMultiplier;
        }
        
    }
    public string GetNextLevelDescription()
    {
        switch (level + 1)
        {
            case 2:
            return ("increase flat heal by 10 (base heal is 10) ");
            break;
            case 3:
            return ("reduce cast time by 1 second (Scales with cast Cooldown)");
            break;
            case 4:
            return (" add a 10 percent percentage heal");
            break;
            case 5:
            return ("increase damage based on 10% of amount healed");
            break;
            default:
            return ("Level List not working in Holy");
            break;
        }
    }
    public void IncreaseLevel()
    {
        level++;
        switch (level)
        {
            case 2:
            healAmountFlat += 10;
            break;
            case 3:
            castcooldown--;
            break;
            case 4:
            healAmountPercent += 10;
            break;
            case 5:
            enableHealToDamage = true;
            break;
            default:
            Debug.Log("Level List not working in Holy");
            break;
        }
    }
}
