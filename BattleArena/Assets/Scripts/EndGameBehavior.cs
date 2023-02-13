using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameBehavior : MonoBehaviour
{
    public GameManagerBehavior gameManager;

    void start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player" && gameManager.Items >= 3)
        {
            gameManager.showWinScreen = true;
            Time.timeScale = 0f; 
        }
    }

}
