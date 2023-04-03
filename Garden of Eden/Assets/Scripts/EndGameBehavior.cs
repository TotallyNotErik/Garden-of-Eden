using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameBehavior : MonoBehaviour
{
    public GameManagerBehavior gameManager;
    public PlayerBehavior movement;
    public EndPlayerMovement endmovement;

    void start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    void OnTriggerEnter(Collider other)
    {
       
        if (other.name == "Player" && gameManager.Items >= 3)
        {
            /*
            while (GameObject.Find("Enemy") != null)
            {
                en = GameObject.Find("Enemy");
                Destroy(en);
            }

            while (GameObject.Find("Enemy(Clone)") != null)
            {
                en = GameObject.Find("Enemy(Clone)");
                Destroy(en);
            }

            while (GameObject.Find("Destroyer(Clone)") != null)
            {
                en = GameObject.Find("Destroyer(Clone)");
                Destroy(en);
            }
            */
            gameManager.showWinScreen = true;
            movement.enabled = false;
            endmovement.enabled = true;
        }
    }

}
