using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Combust : MonoBehaviour
{
    float hitDamage;
    bool peircing;
    float ExplodeTimer;
    Transform Target;
    float timer;
    public float blastRadius = 20;
    bool aquiredTarget;
    private void Start()
    {
     GetTarget();
    }
    private void Update()
    {
        if (Target!= null)
        {
            transform.position = Target.position;
            timer += Time.deltaTime;
        }
        else 
        {
            if(aquiredTarget)
            {
                Explode();
            }
            else
            {
                timer += Time.deltaTime;
            }
           
        }
   
        if (timer >= ExplodeTimer)
        {
            Explode();
        }
    }
    void GetTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Target = enemies[Random.Range(0, enemies.Length)].transform;
        if(Target !=null)
        {
            aquiredTarget = true;
        }
   
    }
    public void GetStats(float damage, bool peirce, float despawnTime)
    {
        hitDamage = damage;
        peircing = peirce;
        ExplodeTimer = despawnTime;
    }
    void Explode()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position,enemy.transform.position);
            if(distance < blastRadius)
            {
                HealthManager health = enemy.GetComponent<HealthManager>();
                health.DecreaseHealthflat(hitDamage);

            }
        }
    }
     

}
