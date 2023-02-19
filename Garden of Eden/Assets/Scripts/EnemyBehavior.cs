using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public GameManagerBehavior gameManager;
    private int _enemyHP = 10;
    public int HP 
    {
        get {return _enemyHP;}
        set 
        {
            _enemyHP = value;
            if(_enemyHP <=0) 
            {Destroy(this.gameObject);
            Debug.Log("Enemy Defeated!");
            gameManager.EnemyCount--;
            }
            else
            {Debug.Log("Enemy Hp:" + _enemyHP);}
        }
    }
    void Start() 
    {
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        gameManager = GameManagerBehavior.instance;
        gameManager.EnemyCount+= 1;
    }
    void OnCollisionEnter(Collision other)
    {
 
        if(other.gameObject.name == "Bullet" || other.gameObject.name == "Bullet(Clone)")
        {
            HP--;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.name == "Player")
	{
		/*Debug.Log("They're Stealing the artifacts! Get Them!");*/
	}
    }

    void OnTriggerExit(Collider other)
    {
        if(other.name == "Player")
	{
		/*Debug.Log("Dammit, we lost them.  Stay on the lookout.");*/
	}
    }
}
