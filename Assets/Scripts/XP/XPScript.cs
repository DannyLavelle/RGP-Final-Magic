using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPScript : MonoBehaviour
{

    public float value;

    private void Start()
    {
        value = Mathf.Floor(1 + (WeaponManager.WeaponManagerInstance.gameTimer / 15));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.tag == "Player")
        {
            //Debug.Log("Collided with xp From XP");
           PlayerXP playerXP = collision.gameObject.GetComponent<PlayerXP>();
           playerXP.GainXP(value);
            Destroy(gameObject);
        }
       
    }

}
