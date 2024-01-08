using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
public enum School
{
    Fire,
    Earth,
    Wind,
    Water,

}
//[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character Data")]

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance = new PlayerStats();
    
    GameObject MenuController;
    LevelUpMenuCOntroller levelUp;
    public GameObject UI;
    public GameObject GameOver;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        MenuController = GameObject.FindGameObjectWithTag("UI");
        levelUp=MenuController.GetComponent<LevelUpMenuCOntroller>();
    }
    [SerializeField]
    public  float castSpeedMultiplier = 1; // remember to minus this for faster casr times so .95 for 5% faster speed
    public  float healthMultiplier = 1;
    public  float speedMultiplier = 1;
    public  float damageMultiplier = 1;
    public float holy5DamageMultiplier = 1;
 

    [SerializeField]
    [Header("School")]
    public  School characterClass;

    [SerializeField]
    [Header("XP")]
    public float level = 1;
    public float CurrentXP = 0;
    private float baseXPToNextLevel = 2;
    public float XPToNextLevel = 2;
    public float XPMultiplier = 2;
    
    public void UpdateStats()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        if(characterClass == School.Fire)
        {
            FireMagicController fire = WeaponManager.WeaponManagerInstance.magicInventory[0].GetComponent<FireMagicController>();
            fire.damage *= (damageMultiplier * healthMultiplier);
            fire.castcooldown*= castSpeedMultiplier;

        }
        HealthManager health = Player.GetComponent<HealthManager>();
        health.ChangeMaxHealthMultiplier(healthMultiplier);
        PlayerMovement movement = Player.GetComponent<PlayerMovement>();
        movement.moveSpeed = movement.basemoveSpeed * speedMultiplier;
    }

    public void DeathSequence()
    {
        Time.timeScale = 0f;
        UI.SetActive(false);
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        AudioSource audio = Player.GetComponent<AudioSource>();
        audio.Pause();
        GameOver.SetActive(true);

    }
}
