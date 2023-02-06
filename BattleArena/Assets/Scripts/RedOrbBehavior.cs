using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOrbBehavior : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Player")
        {

            Destroy(this.transform.parent.gameObject);

            Debug.Log("You Have Collected the Red Artifact!");
        }
    }

}
