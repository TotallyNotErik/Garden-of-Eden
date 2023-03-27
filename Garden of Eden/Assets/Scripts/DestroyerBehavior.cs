using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DestroyerBehavior : MonoBehaviour
{
    public Material hitmat;
    public Material Default;
    public Transform player;
    private UnityEngine.AI.NavMeshAgent agent;

    public GameManagerBehavior gameManager;
    private int _enemyHP = 5;
    public int HP
    {
        get { return _enemyHP; }
        set
        {
            _enemyHP = value;
            this.transform.gameObject.GetComponent<Renderer>().material = hitmat;
            Invoke("ResetMaterial", 0.1f);
            if (_enemyHP <= 0)
            {
                Destroy(this.gameObject);
                Debug.Log("Enemy Defeated!");
                gameManager.EnemyCount--;
            }
            else
            { Debug.Log("Enemy Hp:" + _enemyHP); }
        }
    }
    void ResetMaterial()
    {
        this.transform.gameObject.GetComponent<Renderer>().material = Default;
    }
    void OnCollisionEnter(Collision other)
    {

        if (other.gameObject.name == "Bullet" || other.gameObject.name == "Bullet(Clone)")
        {
            HP--;
        }
    }
    void Start()
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        gameManager = GameManagerBehavior.instance;
        player = GameObject.Find("Player").transform;
        gameManager.EnemyCount += 1;
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        player = GameObject.Find("Player").transform;
        

    }
    void Update()
    {
        agent.destination = player.position;
    }
}
   
