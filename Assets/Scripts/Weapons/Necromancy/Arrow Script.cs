using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float flyspeed = 20;
  
    public GameObject target;
    float despawnTimer;
    float despawnTime = 3;
    float damage = 10;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        despawnTimer+= Time.deltaTime;
        if(despawnTimer >= despawnTime)
        {
            Destroy(gameObject);
        }
    }
    void LaunchArrowTowardsEnemy()
    {
     

        if (target != null)
        {
            // Calculate the direction from arrow's position to the enemy's position
            Vector2 direction = target.transform.position - transform.position;

            // Normalize the direction to get a unit vector
            direction.Normalize();

            // Apply force to the arrow in the direction of the enemy
            rb.AddForce(direction * flyspeed, ForceMode2D.Impulse); ;
        }
        else
        {
            Debug.LogWarning("Enemy not found.");
        }
    }

    public void SetTarget(GameObject Target)
    {
        target = Target;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Deal damage
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            HealthManager health = collision.gameObject.GetComponent<HealthManager>();
            health.DecreaseHealthflat(damage);
        }
    }
}
