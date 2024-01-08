using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BasicFire : MonoBehaviour
{
    FireMagicController magicController ;
    public float flySpeed;
    public float flyTime = 5;
    public float hitDamage;
    private float timer;
    Rigidbody2D rb;
    bool peircing = true;
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
            Destroy(gameObject);
        }
        
        if (rb.velocity.magnitude < 0.1)
        {
            Destroy(gameObject);
        }
       
        //transform.Translate(transform.TransformDirection(Vector2.up)*Time.deltaTime*flySpeed);
      
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
            healthManager.DecreaseHealthflat(hitDamage);
            Debug.Log("DamagedEnemyWithBasicFire");
            if (!peircing)
            {
                Destroy(gameObject);
            }
         
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("basic fire colliding");
        if (collision.gameObject.tag == "Enemy")
        {
            HealthManager healthManager = collision.gameObject.GetComponent<HealthManager>();
            healthManager.DecreaseHealthflat(hitDamage);
            Debug.Log("DamagedEnemyWithBasicFire " + hitDamage);
            if (!peircing)
            {
                Destroy(gameObject);
            }

        }
    }
    public void GetStats(float damage, bool peirce,float despawnTime)
    {
        hitDamage = damage;
        peircing = peirce;
        flyTime = despawnTime;
    }
}
