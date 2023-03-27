using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehavior : MonoBehaviour
{
    public Material hitmat;
    public Material Default;
    public Transform patrolRoute;
    public List<Transform> locations;
    public Transform player;

    private int locationIndex = 0;
    private UnityEngine.AI.NavMeshAgent agent;

    public GameManagerBehavior gameManager;
    private int _enemyHP = 10;
    public int HP 
    {
        get {return _enemyHP;}
        set 
        {
            _enemyHP = value;
            this.transform.gameObject.GetComponent<Renderer>().material = hitmat;
            Invoke("ResetMaterial", 0.1f);
            if (_enemyHP <=0) 
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy Defeated!");
                gameManager.EnemyCount--;
            }

        }
    }

    void ResetMaterial()
    {
        this.transform.gameObject.GetComponent<Renderer>().material = Default;
    }
    void Start() 
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        gameManager = GameManagerBehavior.instance;
        gameManager.EnemyCount += 1;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        InitializePatrolRoute();
        MoveToNextPatrolLocation();

    }
    void Update()
    {
        if (agent.remainingDistance < 0.5f && !agent.pathPending && agent.destination != player.position)
        {
            MoveToNextPatrolLocation();
        }
    }
    void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;
        agent.destination = locations[locationIndex].position;
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            
        }


    }

void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            agent.destination = player.position;
        }

    } 
    void OnCollisionEnter(Collision other)
    {
 
        if(other.gameObject.name == "Bullet" || other.gameObject.name == "Bullet(Clone)")
        {
            HP--;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
	{
            MoveToNextPatrolLocation();
        }
    }
}
