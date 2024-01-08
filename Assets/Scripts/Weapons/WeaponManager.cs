using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    public static WeaponManager WeaponManagerInstance;
    public GameObject[] magicInventory;
    public GameObject[] SecondaryMagic;
   public float gameTimer;
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
    private void Update()
    {
        gameTimer = Time.deltaTime;
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
    public void addSecondary(Magic magicTypeinput)
    {
        foreach(GameObject inventory in SecondaryMagic)
        {
            GameObject magic = Instantiate(inventory);
            MagicTypeScript magic1 = magic.GetComponent<MagicTypeScript>();
            if(magic1.magicType== magicTypeinput)
            {
                magicInventory[magicInventory.Length] = inventory;
            }
            else
            {
                Destroy(inventory);
            }
        }
    }
}
