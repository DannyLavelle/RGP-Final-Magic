using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public static WeaponManager WeaponManagerInstance;
    public GameObject[] magicInventory = new GameObject[5];
    public GameObject fireMagic; 
    private void Awake()
    {
        if (WeaponManagerInstance != null && WeaponManagerInstance != this)
        {
            Destroy(gameObject);
            return;
        }
        WeaponManagerInstance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        AssignStartingWeappn();
    }

   void AssignStartingWeappn()
    {
        switch (PlayerStats.instance.characterClass)
        {
            case School.Fire:
           magicInventory[0] =  Instantiate(fireMagic);
            //TODO assign image to ui
            break;
        }
    }
}
