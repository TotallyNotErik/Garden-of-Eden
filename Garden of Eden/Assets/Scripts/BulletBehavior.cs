using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public GameObject hitParticles;
    public GameObject enemyHitParticles;
    void Start()
    {
      
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Enemy(Clone)" || other.gameObject.name == "Destroyer(Clone)" || other.gameObject.name == "Enemy")
            Instantiate(enemyHitParticles, this.transform.position, this.transform.rotation);
        else
            Instantiate(hitParticles, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject); 
    }
}
 
   