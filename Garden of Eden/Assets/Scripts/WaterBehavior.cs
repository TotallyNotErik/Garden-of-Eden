using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehavior : MonoBehaviour
{
    public PlayerBehavior player;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
        {
            player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
            player.moveMultiplier = 0.5f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.name == "Player")
        {
            player = GameObject.Find("Player").GetComponent<PlayerBehavior>();
            player.moveMultiplier = 1f;
        }
    }
}
