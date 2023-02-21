using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public GameManagerBehavior gameManager;
    public EnemyBehavior enemy;
    public DestroyerBehavior destroyer;
    public float jumpVelocity = 5f;
    public float moveSpeed = 10f;
 	public float rotateSpeed = .25f;
    public float gravityVel = 9.8f;
    public float distanceToGround = 0.1f;
    public GameObject barrier;

    public bool swimming = false;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    public LayerMask groundLayer;
    private Vector3 input;
    private float camInput;

	private Rigidbody _rb;
    private CapsuleCollider _col;

//    private float mover = 1f;
    public float moveMultiplier = 1f;
   /* {
        get { return mover; }
        set { mover = value;
            Debug.Log("Movement Changed");
        }
    }
 */
    
    private float charge = 0.0f;
    private float maxCharge = 5.0f;
    private bool charging = false;
    private bool bashing = false;
    private float bashStart = 0f;
    private float timeCharged = 0f;

    private bool counter = false;
    private float counterDuration = 0.1f;

    private float counterboostDur = 3f;
    private float counterboosttime = 0.0f;
    


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        barrier = GameObject.Find("Barrier");
        barrier.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        { input.z = 1f; }
        else if (Input.GetKey(KeyCode.S))
           { input.z = -1f; }
        else { input.z = 0f; }

        if (Input.GetKey(KeyCode.A))
        { input.x = -0.75f; }
        else if (Input.GetKey(KeyCode.D))
        { input.x = 0.75f; }
        else { input.x = 0f; }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        { _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse); }

        if (Input.GetKey(KeyCode.E) && gameManager.BlueArtifact && Time.time >= gameManager.counterCD + gameManager.countertime && charging == false && bashing == false)
        {
            counter = true;
            gameManager.countertime = Time.time;
            barrier.SetActive(true);
        }
        else if (Time.time >= gameManager.countertime + counterDuration)
        {
            counter = false;
            barrier.SetActive(false);
        }

        if (!charging && Time.time > counterboosttime + counterboostDur && !swimming)
            moveMultiplier = 1f;

        if (Input.GetKeyDown(KeyCode.LeftShift) && gameManager.YellowArtifact && gameManager.RedArtifact && gameManager.BlueArtifact)
        {
            if (((this.transform.position + this.transform.rotation * input * 5).x < 30 && (this.transform.position + this.transform.rotation * input * 5).x > -30 && (this.transform.position + this.transform.rotation * input * 5).z < 30 && (this.transform.position + this.transform.rotation * input * 5).z > -30) && ((this.transform.position + 5 * this.transform.forward).x < 30 && (this.transform.position + 5 * this.transform.forward).x > -30 && (this.transform.position + 5 * this.transform.forward).z < 30 && (this.transform.position + 5 * this.transform.forward).z > -30) && Time.timeAsDouble > gameManager.dashTimer + gameManager.dashCooldown)
            {
                /*this.transform.position += 5 * this.transform.forward;*/
                if (input == new Vector3(0,0,0))
                    this.transform.position += 5 * this.transform.forward;
                this.transform.position += this.transform.rotation * input * 5;

                gameManager.dashTimer = Time.timeAsDouble;
            }
           
        }

        if (gameManager.RedArtifact)
        {
            if (Input.GetMouseButtonDown(0) && gameManager.Magazine > 0)
            {
                GameObject newBullet = Instantiate(bullet,  this.transform.position + this.transform.rotation * new Vector3(0.6f, 0, 0), this.transform.rotation) as GameObject;
                Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
                bulletRB.velocity = this.transform.forward * bulletSpeed;
                gameManager.Magazine--;
            }

            if (Time.timeAsDouble >= gameManager.reloadTimer + gameManager.reloadTime && gameManager.Magazine == 0)
                gameManager.Magazine = 8;
        }
	    if (Input.GetKey(KeyCode.R) && gameManager.Magazine != 8 && gameManager.Magazine != 0)
        {
            gameManager.Magazine = 0;
        }
        if (Input.GetMouseButton(1) && gameManager.YellowArtifact && Time.time > 3 + gameManager.chargeTimer + (gameManager.chargeTime * gameManager.cdMultiplier))
        {
		    if (Input.GetMouseButtonDown(1)) {
			    charge = Time.time;
		    }
             moveMultiplier = 0.25f;
           	 charging = true;


        }
        else
        {
           	 if (charging && Time.time - charge > maxCharge)
           	 {
                  _rb.velocity = this.transform.forward * moveSpeed * maxCharge;
                  charging = false;
                  gameManager.cdMultiplier = maxCharge;
                  gameManager.chargeTimer = Time.time;
                  timeCharged = 5f;
                  bashing = true;
                  bashStart = Time.time;
                  moveMultiplier = 1f;
            }
             else if (charging)
             {
                _rb.velocity = this.transform.forward * moveSpeed * (Time.time - charge + 1);
                charging = false;
                timeCharged = Time.time - charge;
                gameManager.cdMultiplier = (Time.time - charge);
                gameManager.chargeTimer = Time.time;
                bashing = true;
                bashStart = Time.time;
                moveMultiplier = 1f;
            }
             
       	}
        if (bashing == true && Time.time > 0.5 + (bashStart + 0.1 * timeCharged)) 
        {
            bashing = false;
        }
	
        _rb.AddForce(-Vector3.up * gravityVel * Time.deltaTime, ForceMode.Impulse);

        camInput = Input.GetAxis("Horizontal") * gameManager.sensitivity *rotateSpeed;
      
        Vector3 rotation = Vector3.up * camInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);
    }


    void FixedUpdate()
    {
        if (this.transform.position.y <= -5)
            this.transform.position = new Vector3(0, 4, 0);
        /*_rb.MovePosition(this.transform.position + input.z * this.transform.forward * moveSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(this.transform.position + input.x * this.transform.right * moveSpeed * Time.fixedDeltaTime);
        */
        if (((transform.position + this.transform.rotation * input * moveSpeed * Time.fixedDeltaTime).x >= -30) && ((transform.position + this.transform.rotation * input * moveSpeed * Time.fixedDeltaTime).x <= 30) && ((transform.position + this.transform.rotation * input * moveSpeed * Time.fixedDeltaTime).z >= -30) && ((transform.position + this.transform.rotation * input * moveSpeed * Time.fixedDeltaTime).z <= 30))
        { /* transform.position += this.transform.rotation * input * moveSpeed * moveMultiplier * Time.fixedDeltaTime; */
            _rb.MovePosition(this.transform.position + input.z * this.transform.forward * moveSpeed * moveMultiplier * Time.fixedDeltaTime + input.x * this.transform.right * moveSpeed * moveMultiplier * Time.fixedDeltaTime);
        }
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.name == "Enemy" || other.gameObject.name == "Enemy(Clone)" || other.gameObject.name == "Destroyer(Clone)")
	    {
            if(other.gameObject.name == "Destroyer(Clone)")
                destroyer = other.gameObject.GetComponent<DestroyerBehavior>();
            else
                enemy = other.gameObject.GetComponent<EnemyBehavior>();

            if (bashing == false && counter == false) 
            {
                gameManager.HP--;
                if (other.gameObject.name == "Destroyer(Clone)")
                    gameManager.HP--;

                if (!charging)
                {
                    _rb.AddForce(other.gameObject.transform.forward * moveSpeed * 0.75f, ForceMode.Impulse);
                    _rb.AddForce(Vector3.up * jumpVelocity * 0.75f, ForceMode.Impulse);
                }
            }
            else if(counter == true)
            {
                gameManager.HP+=2;
                if (other.gameObject.name == "Destroyer(Clone)")
                    destroyer.HP-=2 ;
                else
                    enemy.HP-= 2;
                //gameManager.countertime -= (gameManager.counterCD / 2);
                moveMultiplier = 1.5f;
                counterboosttime = Time.time;
            }
            else if (bashing == true)
            {
                _rb.velocity = -this.transform.forward * moveSpeed;
//                other.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * moveSpeed);

                if (other.gameObject.name == "Destroyer(Clone)")
                    destroyer.HP--;
                else
                    enemy.HP--;
                if (timeCharged >= 2.5)
                {

                    if (other.gameObject.name == "Destroyer(Clone)")
                        destroyer.HP--;
                    else
                        enemy.HP--;
                }
            }
	    }
    }
   
}
