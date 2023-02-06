using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBehavior : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Player")
        {

            Destroy(this.transform.parent.gameObject);

            Debug.Log("Health Restored!");
        }
    }

}