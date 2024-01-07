using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public static WeaponManager WeaponManagerInstance;
    public GameObject[] magicInventory = new GameObject[5];
    public GameObject[] SecondaryMagic;
    
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

    public List<GameObject> GetUnusedSecondary()
    {
        List<GameObject> freeMagic = new List<GameObject>(SecondaryMagic);
        //foreach(GameObject item in SecondaryMagic)
        //{
        //    FreeMagic.Add(item);
        //}
        foreach (GameObject inventory in magicInventory)
        {
            if (inventory != null && freeMagic.Contains(inventory))
            {
                freeMagic.Remove(inventory);
            }
        }
        return freeMagic;
    }
}
