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
    public GameObject levelUpMenu;
        GameObject Player;
    HealthManager health;
    //To Generate
    GameObject[] magicSlots;
    PlayerStats playerStats;
    Magic magicToLearn;
    //Magic Types
    FireMagicController fireMagic;
    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        health = Player.GetComponent<HealthManager>();
        playerStats = PlayerStats.instance;
    }
    public void GenerateLevelUpMenu()
    {
        levelUpMenu.SetActive(true);
        Time.timeScale = 0f;
        magicSlots = WeaponManager.WeaponManagerInstance.magicInventory;
        Option1Body.text = "";
        Option2Body.text = "";
        Option3Body.text = "";
        Option1Header.text = "";
        Option2Header.text = "";
        Option3Header.text = "";
        switch (playerStats.characterClass)
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
                if(fireMagic.level == fireMagic.evolveLevel)
                {

                }
                else
                {
                    Option1Header.text = "Upgrade Fire Magic";
                    Option1Body.text = "Upgrade Fire Magic to Level " + (fireMagic.level + 1) + " Effects: " + fireMagic.GetNextLevelDescription();
                }
         
            }

            break;
            //TODO other magics
        }
        if(Option2Header.text == "")
        {
            bool o2Filled = CheckSecondary();
            if(o2Filled == false)
            {

                GenerateStatBoost(2);
            }

        }
        if(Option3Header.text == "")
        {
            bool o3Filled = GenerateUpgradeOption();
            if (o3Filled == false)
            {

                GenerateStatBoost(3);
            }
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
        switch(playerStats.characterClass)
        {
            case School.Fire:
            level = fireMagic.level;
            evoLevel = fireMagic.evolveLevel;
            Path1Header = "Evolve: FlameThrower";
            Path2Header = "Evolve: FireBall";
            Path3Header = "Evolve: Combustion";
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
            List<GameObject> potentialMagic = WeaponManager.WeaponManagerInstance.GetUnusedSecondary();
            if(potentialMagic.Count > 0)
            {
                GameObject elementToDisplay;
                int randomIndex = Random.Range(0, potentialMagic.Count);
                elementToDisplay = Instantiate(potentialMagic[randomIndex]);
                MagicTypeScript magicType = elementToDisplay.GetComponent<MagicTypeScript>();
                Magic displayMagicType = magicType.magicType;
                Destroy(elementToDisplay);
                magicToLearn = displayMagicType;
                Option2Header.text = ("Learn " + displayMagicType.ToString() + " Magic");

                switch(displayMagicType)
                {
                    case Magic.Necromancy:
                    Option2Body.text = ("Capture the souls of defeated enemies to summon hordes of zombies and even bow weilding skeletons at later levels.");
                    break;
                    case Magic.Holy:
                    Option2Body.text =  ("Channel the power of the divine to heal your wounds.");
                    break;
                    //add other submagic descriptions here

                }
                return true;
            }
            else
            {
                return false;
            }
            
        }
        else
        {
            return false;
        }
    }


    void GenerateStatBoost(int Option)
    {
       int randomEffect =  Random.Range(0, 5);
        string header = "";
            string body = "";
        switch(randomEffect)
        {
            case 0:
            header = "Recover";
            body = "Recover 20 percent of your HP";
                break;
            case 1:
            header = "Health Boost";
            body = "Increase max health by 10 percent";
                break;
            case 2:
            header = "Damage Boost";
            body = "Increase your damage by 10 percent";
            break;
            case 3:
            header = "Move Speed Boost";
            body = "Increase your movement speed by 10 percent";
            break;
            case 4:
            header = "Cast Speed Boost";
            body = "Reduces your time to cast a spell by 5 percent";
            break;
            default:
            header = "Couldn't generate stat Boost";
            body = "WHHYYYYY";
                break;
        }
        if(Option == 1)
        {
            Option1Header.text = header;
            Option1Body.text = body;
        }
        else if(Option == 2)
        {
            Option2Header.text = header;
            Option2Body.text=body;
        }
        else
        {

            Option3Header.text = header;
            Option3Body.text = body;
        }
    }
    bool GenerateUpgradeOption()
    {
        int numberofSecondarysMagics = WeaponManager.WeaponManagerInstance.magicInventory.Length;
        if (numberofSecondarysMagics > 1)
        {
            List<GameObject> viableUpgrades = new List<GameObject>();
            bool isFirstElement = true;
            foreach (GameObject spell in WeaponManager.WeaponManagerInstance.magicInventory)
            {
                if(isFirstElement)
                {
                    isFirstElement = false;
                    continue;
                }
                if (spell !=null)
                {
                    MagicTypeScript magic = spell.GetComponent<MagicTypeScript>();
                    switch (magic.magicType)
                    {
                        case Magic.Necromancy:
                        NecromancyController necromancyController = spell.GetComponent<NecromancyController>();
                        if (necromancyController.level < necromancyController.maxLevel)
                        {
                            viableUpgrades.Add(spell);

                        }
                        break;
                        case Magic.Holy:
                        HolyMagicController holyMagicController = spell.GetComponent<HolyMagicController>();
                        if (holyMagicController.level < holyMagicController.maxLevel)
                        {
                            viableUpgrades.Add(spell);
                        }
                        break;

                        //add other secondary magics here
                    }
                }
                

            }
            if(viableUpgrades.Count == 0)
            {
                return false;
            }
            else if(viableUpgrades.Count == 1)
            {
                AddUpgradeOption(viableUpgrades[0]);
                return true;
            }
            else
            {
                AddUpgradeOption(viableUpgrades[Random.Range(0, viableUpgrades.Count)]);
                return true;
            }
        }
        else
        {
            return false;
        }

        void AddUpgradeOption(GameObject magicToUpgrade)
        {
            MagicTypeScript magicType = magicToUpgrade.GetComponent<MagicTypeScript>();
            string body = "";
            string header = "";
            switch(magicType.magicType)
            {
                case Magic.Necromancy:
                    NecromancyController necromancyController = magicToUpgrade.GetComponent<NecromancyController>();
                header = "Upgrade Necromancy";
                body = ("Upgrade Necromancy to Level " + (necromancyController.level + 1) + " Effects: " + necromancyController.GetNextLevelDescription());
                    break;
                case Magic.Holy:
               
                HolyMagicController holyMagicController = magicToUpgrade.GetComponent<HolyMagicController>();
                header = "Upgrade Holy Magic";
                body = "Upgrade Holy Magic Level " + (holyMagicController.level + 1) + " Effects: " + holyMagicController.GetNextLevelDescription();
                break;
                //add other secondary magic here
                default:
                    header ="Can't find magic type";
                body = "in add upgrade option";
                    break;


            }
            Option3Header.text = header;
            Option3Body.text = body;
        }
    }

    public void Option1Clicked()//time for if statement hell 
    {
        string header = Option1Header.text;

        if (header.Contains ("Upgrade"))
        {
            LevelPrimary();
        }
        else if(header.Contains("Boost"))
        {
            StatBoostHandeler(header);
        }
        else if(header.Contains("Recover"))
        {
            HealPlayer();
        }
        if(header.Contains("Evolve"))
        {
            EvolvePrimary(1);
        }

        Time.timeScale = 1f;
        levelUpMenu.SetActive(false);
    }
    public void Option2Clicked()//time for if statement hell 
    {
        string header = Option2Header.text;

        if (header.Contains("Learn"))
        {
            WeaponManager.WeaponManagerInstance.addSecondary(magicToLearn);
        }
        else if (header.Contains("Boost"))
        {
            StatBoostHandeler(header);
        }
        else if (header.Contains("Recover"))
        {
            HealPlayer();
        }
        if (header.Contains("Evolve"))
        {
            EvolvePrimary(2);
        }

        Time.timeScale = 1f;
        levelUpMenu.SetActive(false);
    }
    public void Option3Clicked()//time for if statement hell 
    {
        string header = Option3Header.text;

        if (header.Contains("Upgrade"))
        {
            UpgradeSecondary(header);
        }
        else if (header.Contains("Boost"))
        {
            StatBoostHandeler(header);
        }
        else if (header.Contains("Recover"))
        {
            HealPlayer();
        }
        if (header.Contains("Evolve"))
        {
            EvolvePrimary(2);
        }
        Time.timeScale = 1f;
        levelUpMenu.SetActive(false);
    }

    void LevelPrimary()
    {
        switch(playerStats.characterClass)
        {
            case School.Fire:
            FireMagicController fireMagic = WeaponManager.WeaponManagerInstance.magicInventory[0].GetComponent<FireMagicController>();
            fireMagic.IncreaseLevel();
            break;
            //add othe primary magic here
        }
    }

    void StatBoostHandeler(string header)
    {
        if(header.Contains("Health"))
        {
            PlayerStats.instance.healthMultiplier += .1f;

            //Create an update stats on player 
        }
        else if(header.Contains("Damage"))
        {
            PlayerStats.instance.damageMultiplier += .1f;
            //Create an update stats on player 
        }
        else if (header.Contains("Move Speed"))
        {
            PlayerStats.instance.speedMultiplier += .1f;
            //Create an update stats on player 
        }
        else if (header.Contains("Cast Speed"))
        {
            PlayerStats.instance.castSpeedMultiplier -= .05f;
            //Create an update stats on player 
        }
        //updateplayer stats probably via a player stat manager script 
    }
    void HealPlayer()
    {
      
        health.IncreaseHealthPercent(.2f);
    }
    void EvolvePrimary(int option)
    {
       switch (playerStats.characterClass)
        {
            case School.Fire:
            
                if(option == 1)
            {
                fireMagic.EvolveWeapon("Flamethrower");
            }
                else if (option == 2)
            {
                fireMagic.EvolveWeapon("Fireball");
            }
                else if (option == 3)
            {
                fireMagic.EvolveWeapon("Combust");
            }
            break;//
        }
       
    }
    void UpgradeSecondary(string Header)
    {
        if(Header.Contains("Necromancy"))
        {
            NecromancyController necromancy = GameObject.FindAnyObjectByType<NecromancyController>();
            necromancy.IncreaseLevel();

        }
        else if(Header.Contains("Holy"))
        {
            HolyMagicController holy = GameObject.FindAnyObjectByType<HolyMagicController>();   
            holy.IncreaseLevel();
        }
    }
}
