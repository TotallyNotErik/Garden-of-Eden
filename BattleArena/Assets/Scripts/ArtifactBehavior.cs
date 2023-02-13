using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactBehavior : MonoBehaviour
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

            Debug.Log("You Have Collected the Blue Artifact!");

            gameManager.Items += 1;
            gameManager.BlueArtifact = true;
        }
    }

}
