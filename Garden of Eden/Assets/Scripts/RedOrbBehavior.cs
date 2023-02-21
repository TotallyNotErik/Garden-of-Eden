using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedOrbBehavior : MonoBehaviour
{
    public GameManagerBehavior gameManager;
    public GameObject Destroyer;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Player")
        {

            Destroy(this.transform.parent.gameObject);

            Debug.Log("You Have Collected the Red Artifact!");
            GameObject newEnemy = Instantiate(Destroyer, new Vector3(-20f, 1.5f, 20f), Quaternion.identity) as GameObject;

            gameManager.Items += 1;
            gameManager.RedArtifact = true;
        }
    }

}
