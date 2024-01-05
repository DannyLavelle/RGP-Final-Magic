using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpMenuCOntroller : MonoBehaviour
{

    //Level Up Menu Parts 
    public TMP_Text Option1Header;
    public TMP_Text Option2Header;
    public TMP_Text Option3Header;
    public TMP_Text Option1Body;
    public TMP_Text Option2Body;
    public TMP_Text Option3Body;

    //To Generate
    GameObject[] magicSlots;
    PlayerStats player;

    //Magic Types
    FireMagicController fireMagic;
    private void Start()
    {
        player = PlayerStats.instance;
    }
    public void GenerateLevelUpMenu()
    {
        magicSlots = WeaponManager.WeaponManagerInstance.magicInventory;
        Option1Body.text = "";
        Option2Body.text = "";
        Option3Body.text = "";
        Option1Header.text = "";
        switch(player.characterClass)
        {
            case School.Fire:
            fireMagic = magicSlots[0].GetComponent<FireMagicController>();
            bool canEvo = CheckForEvo();
            if(canEvo)
            {
                break;
            }
            else
            {
                Option1Header.text = "Level Fire Magic";
                Option1Body.text = "TODO add fire magic description";
            }

            break;
            //TODO other magics
        }
        if(Option2Header.text == "")
        {
            bool o2Filled = CheckSecondary();
            if(o2Filled == false)
            {

                //TODO Get statboost for option 2
            }

        }
        if(Option3Header.text == "")
        {
            //TODO Get upgrade for secondary weapon or statboost
        }
    }

    bool CheckForEvo()
    {
        float level = 0;
        float evoLevel = 0;


        string Path1Header = "";
        string Path2Header = "";
        string Path3Header = "";
        string Path1Body = "";
        string Path2Body = "";
        string Path3Body = "";
        switch(player.characterClass)
        {
            case School.Fire:
            level = fireMagic.level;
            evoLevel = fireMagic.evolveLevel;
            Path1Header = "FlameThrower";
            Path2Header = "FireBall";
            Path3Header = "Combustion";
            Path1Body = "A short range flamethrower what burns everythin in it's area";
            Path2Body = "A fireball that explodes dealing damage to all enemie in it's explosion radius";
            Path3Body = "Mark an enemy after some time or after enemies are defeated they explode dealing massive damage in a large radius ";
            break;
            //TODO other magic types
           
        }
        if (level == evoLevel - 1)
        {
            Option1Header.text = Path1Header;
            Option2Header.text = Path2Header;
            Option3Header.text = Path3Header;
            Option1Body.text = Path1Body;
            Option2Body.text = Path2Body;
            Option3Body.text = Path3Body;
            return true;

        }
        else
        {
            return false;
        }
       
    }

    bool CheckSecondary()
    {
        bool emptyslot = false;
        
        int emptySlotLocation = 0;
        magicSlots = WeaponManager.WeaponManagerInstance.magicInventory;
        for(int i = 1; i < magicSlots.Length; i++)
        {
            if(magicSlots[i] == null)
            {
                emptyslot = true;
                emptySlotLocation = i;
                break;
            }
            
        }
        if (emptyslot)
        {
            //TODO make secondary magic and make a random selecter 
            return true;
        }
        else
        {
            return false;
        }
    }
    
}
