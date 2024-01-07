using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum School
{
    Fire,
    Earth,
    Wind,
    Water,

}
[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character Data")]
public class PlayerStats : ScriptableSingleton<PlayerStats>
{
    
    private void Awake()
    {
       
    }

    [SerializeField]
    [Header("Base Stats")]
    public  float baseHealthMultiplier = 1;
    public  float baseSpeedMultiplier = 1;
    public  float baseDamageMultiplier = 1;
    public  float basePickupRangeMultoiplier = 1;

    [Header("Level Stats")]
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
    
    
}
