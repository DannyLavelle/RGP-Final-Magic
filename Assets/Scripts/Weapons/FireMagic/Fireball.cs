using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    FireMagicController magicController;
    public float flySpeed;
    public float flyTime = 5;
    public float hitDamage;
    private float timer;
    Rigidbody2D rb;
    bool peircing = true;
    public float blastRadius = 2.5f;
    public GameObject explosionParticles;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        //gameObject.transform.Rotate(0f, 0f, -90f);
        magicController = FindAnyObjectByType<FireMagicController>();

        rb.AddForce(magicController.castPoint.transform.up * flySpeed, ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= flyTime)
        {
            Debug.Log("despawn because timeout");
            Destroy(gameObject);
        }

        if (rb.velocity.magnitude < 0.1)
        {
            Debug.Log("despawn because slow");
            Destroy(gameObject);
        }

        //transform.Translate(transform.TransformDirection(Vector2.up)*Time.deltaTime*flySpeed);

    }
    private void OnCollisionEnter(Collision collision)
    {
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if(collision.gameObject.tag == "Enemy")
        {
            Explode();
            Debug.Log("Boom from fireball");
        }
    }
    public void GetStats(float damage, bool peirce, float despawnTime)
    {
        hitDamage = damage;
        peircing = peirce;
        flyTime = despawnTime;
    }
    void Explode()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < blastRadius)
            {
                HealthManager health = enemy.GetComponent<HealthManager>();
                health.DecreaseHealthflat(hitDamage);

            }
        }
        Instantiate(explosionParticles, gameObject.transform.position, gameObject.transform.rotation);
        Destroy(gameObject);
    }
}
