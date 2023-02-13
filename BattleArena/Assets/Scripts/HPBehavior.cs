using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBehavior : MonoBehaviour
{
    public GameManagerBehavior gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Player")
        {

            Destroy(this.transform.parent.gameObject);

            Debug.Log("Health Boosted!");

            gameManager.HP += 5;
        }
    }

}