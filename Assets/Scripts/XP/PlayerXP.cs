using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    PlayerStats stats;
    public GameObject UI;
   LevelUpMenuCOntroller levelUpMenuController;
    private void Start()
    { 
        UI = GameObject.FindGameObjectWithTag("UI");
        levelUpMenuController = UI.GetComponent<LevelUpMenuCOntroller>();
        stats = PlayerStats.instance;
    }
    public void GainXP(float value)
    {
        stats.CurrentXP += value;
        if(stats.CurrentXP >= stats.XPToNextLevel)
        {
            stats.level++;

          levelUpMenuController.GenerateLevelUpMenu();
            stats.CurrentXP -= stats.XPToNextLevel;
            stats.XPToNextLevel *= stats.XPMultiplier;
        }

        //updateui
    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag == "XP")
    //    {
    //        Debug.Log("From Player");
    //        XPScript xPScript = collision.gameObject.GetComponent<XPScript>();
            
    //       GainXP(xPScript.value);
    //    }
    //    Destroy(collision .gameObject);
    //}
}
