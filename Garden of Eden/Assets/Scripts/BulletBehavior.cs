using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    
    void Start()
    {
      
    }
  
    void OnCollisionEnter(Collision other)
    {

        Destroy(this.gameObject,0.1f); 
    }
}
 
   