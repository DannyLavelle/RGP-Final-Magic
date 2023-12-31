using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BasicFire : MonoBehaviour
{
    FireMagicController magicController ;
    public float flySpeed;
    public float flyTime = 5;
    public float damage;
    private float timer;
    Rigidbody2D rb;
    bool peircing = true;
    // Start is called before the first frame update
    void Start()
    {
         rb = gameObject.GetComponent<Rigidbody2D>();
        
        //gameObject.transform.Rotate(0f, 0f, -90f);
        magicController = FindAnyObjectByType<FireMagicController>();

        rb.AddForce(magicController.player.transform.up * flySpeed, ForceMode2D.Impulse);

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
        if (peircing)
        {
            //Do Damage
        }
        else
        {
            //Do Damage
            Destroy(gameObject);
        }
    }
}
