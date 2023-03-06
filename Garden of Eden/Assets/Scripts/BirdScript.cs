using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    public Transform target;
    private bool scared = false;
    private float birdtimer = 0f;
    private float birdreturn = 15f;
    private Vector3 pos;
    public GameObject bird;

    void Start()
    {
        target = GameObject.Find("Player").transform;
        pos = this.transform.position;
    }

    void Update()
    {
        if (scared)
            this.transform.position += this.transform.rotation * new Vector3(0, 20, 15) * Time.deltaTime;
        else
            this.transform.LookAt(target);

        if (Time.time > birdtimer + birdreturn && scared)
        {
            GameObject newbird = Instantiate(bird, pos, this.transform.rotation) as GameObject;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" || other.gameObject.name == "Bullet(Clone)")
        {
            scared = true;
            birdtimer = Time.time;
        }

    }
}
