using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Combust : MonoBehaviour
{
    float hitDamage = 300;
    bool peircing;
    float ExplodeTimer = 10;
    Transform Target;
    float timer;
    public float blastRadius = 7;
    bool aquiredTarget;
    public GameObject explosionParticles;
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
        Target = enemies[Random.Range(0, enemies.Length -1)].transform;
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
            //Debug.Log(distance);
            if(distance < blastRadius)
            {
                HealthManager health = enemy.GetComponent<HealthManager>();
                health.DecreaseHealthflat(hitDamage);
                //Debug.Log("combust Explode Damaging " + enemy+ " For " + hitDamage + " damage");

            }
        }
        Instantiate(explosionParticles, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
     

}
