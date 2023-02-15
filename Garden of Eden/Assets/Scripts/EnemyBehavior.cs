using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
	{
		Debug.Log("They're Stealing the artifacts! Get Them!");
	}
    }

    // Update is called once per frame
    void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
	{
		Debug.Log("Dammit, we lost them.  Stay on the lookout.");
	}
    }
}
