using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPArticleScript : MonoBehaviour
{
    ParticleSystem ps;
   public  GameObject ParticleSys;// Start is called before the first frame update
    void Start()
    {
        ps = ParticleSys.GetComponent<ParticleSystem>();
        Destroy(gameObject, ps.startLifetime);
    }

   
}
