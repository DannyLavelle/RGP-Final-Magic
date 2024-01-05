using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    GameObject[] enemies;
    GameObject target;
    public GameObject Arrow;
    public float attackRange = .1f;
    public float moveSpeed = 3;
    float timer;
    float attackCooldown = 1;
    float despawnTimer;
    float despawnTime = 45;
    bool canAttack;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
            timer += Time.deltaTime;
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
        GameObject Shot = Instantiate(Arrow);
        ArrowScript arrowScript = Shot.GetComponent<ArrowScript>();
        arrowScript.SetTarget(target);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(canAttack)
        {
            //Do Damage
        }
    }
}
