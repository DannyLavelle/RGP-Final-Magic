using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum School
{
    Fire,
    Earth,
    Wind,
    Water,

}
[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character Data")]
public class PlayerStats : ScriptableObject
{

    
    [SerializeField]
    [Header("Base Stats")]
    public static int baseHealthMultiplier = 1;
    public static float baseSpeedMultiplier = 1;
    public static int baseDamageMultiplier = 1;
    public static float basePickupRangeMultoiplier = 1;

    [Header("Level Stats")]
    [SerializeField]
    public static float castSpeedMultiplier = 1; // remember to minus this for faster casr times so .95 for 5% faster speed
    public static int healthMultiplier = 1;
    public static float speedMultiplier = 1;
    public static int damageMultiplier = 1;
    public static float pickupRangeMultiplier = 1;

    [SerializeField]
    [Header("School")]
    public static School characterClass;
}
