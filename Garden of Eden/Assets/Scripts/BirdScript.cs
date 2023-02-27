using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void LateUpdate()
    {

	    this.transform.LookAt(target);

    }
}
