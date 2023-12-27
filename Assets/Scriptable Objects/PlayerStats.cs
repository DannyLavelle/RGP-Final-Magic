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

    
    [Header("Base Stats")]
    public static int baseHealth;
    public static float baseSpeed;
    public static int baseDamage;
    public static float basePickupRange;

    [Header("School")]
    public static School characterClass;
}
