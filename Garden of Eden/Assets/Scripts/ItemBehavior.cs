using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameManagerBehavior gameManager;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    void OnCollisionEnter(Collision collision)
    {

 	    if(collision.gameObject.name == "Player")
 	    {

 	        Destroy(this.transform.parent.gameObject);

 	        Debug.Log("You have collected the Yellow Artifact!");

            gameManager.Items += 1;
            gameManager.YellowArtifact = true;

        }	
    }

}
