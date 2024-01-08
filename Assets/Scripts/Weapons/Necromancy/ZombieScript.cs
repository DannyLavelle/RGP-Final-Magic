using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    GameObject[] enemies;
    GameObject target;
    public GameObject Arrow;
    public float attackRange = .4f;
    public float moveSpeed = 3;
    float timer;
    float attackCooldown = 1;
    float despawnTimer;
    float despawnTime = 20;
    bool canAttack;
    public float damage = 10;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer+=Time.deltaTime;
        if (target == null)
        {
            GetTarget();
        }
        if (GetDistance() >= attackRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
        
        }

        if (timer >= attackCooldown)
        {
            Attack();
            timer = 0;
        }
        despawnTimer += Time.deltaTime;
        if (despawnTimer >= despawnTime)
        {
            Destroy(gameObject);
        }
    }
    void GetTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int rand = Random.Range(0, enemies.Length);
        target = enemies[rand];
    }
    float GetDistance()
    {
        float distance = Vector2.Distance(gameObject.transform.position, target.transform.position);
        return distance;
    }
    void Attack()
    {
        canAttack = true;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(canAttack && collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Attacked enemy");
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
            healthManager.DecreaseHealthflat(damage);
            canAttack = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (canAttack && collision.gameObject.tag == "Enemy")
        {
            Debug.Log("xombie attack");
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
            healthManager.DecreaseHealthflat(damage);
            canAttack = false;
        }
    }
}
