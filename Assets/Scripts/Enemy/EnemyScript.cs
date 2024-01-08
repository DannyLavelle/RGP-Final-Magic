using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyScript : MonoBehaviour
{
    GameObject player;
    public GameObject projectile;
    public bool Ranged = false;
    public float attackRange = 0.4f;
    public float moveSpeed = 2;
    public float attackCooldown = 1;
    float timer;
    bool canAttack = false;
    float damage = 10;
    void Start()
    {
        GetStats();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!Ranged)
        {
            timer += Time.deltaTime;
        }
        if(GetDistance() > attackRange)
        {

            
            transform. position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (Ranged)
            {
                timer += Time.deltaTime;
            }
     
        }

        if (timer >= attackCooldown)
        {
            Attack();
            timer = 0;
        }
    }
    float GetDistance()
    {
        float distance = Vector2.Distance(gameObject.transform.position, player.transform.position);
        return distance;
    }
    void Attack()
    {
        canAttack = true;
        if (Ranged)
        {
            Instantiate(projectile, transform);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided");
        if (canAttack && collision.gameObject.tag == "Player")
        {
            Debug.Log("AttackingPlayer");
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
            healthManager.DecreaseHealthflat(damage);
            canAttack = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collided Trigger");
        if (canAttack && collision.gameObject.tag == "Player")
        {
            Debug.Log("AttackingPlayer");
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
            healthManager.DecreaseHealthflat(damage);
            canAttack = false;
        }
    }
    void GetStats()
    {
        float gamestage = Mathf.Floor(WeaponManager.WeaponManagerInstance.gameTimer / 15);
        float multiplier = Mathf.Pow(1.05f, gamestage);
        damage *= multiplier;
        moveSpeed *= multiplier;
        HealthManager health = gameObject.GetComponent<HealthManager>();
        health.ChangeMaxHealthMultiplier(multiplier);
    }
}
