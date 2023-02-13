using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public GameManagerBehavior gameManager;

    public float jumpVelocity = 5f;
    public float moveSpeed = 10f;
 	public float rotateSpeed = .25f;
    public float gravityVel = 9.8f;
    public float distanceToGround = 0.1f;

    public GameObject bullet;
    public float bulletSpeed = 100f;

    public LayerMask groundLayer;
    private Vector3 input;
    private float camInput;

	private Rigidbody _rb;
    private CapsuleCollider _col;




    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            input.z = 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            input.z = -1f;
        }
        else { input.z = 0f; }

        if (Input.GetKeyDown(KeyCode.Space) && gameManager.BlueArtifact && IsGrounded())
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && gameManager.YellowArtifact)
        {
            if ((this.transform.position + 5 * this.transform.forward).x < 14.25 && (this.transform.position + 5 * this.transform.forward).x > -14.25 && (this.transform.position + 5 * this.transform.forward).z < 14.25 && (this.transform.position + 5 * this.transform.forward).z > -14.25 && Time.timeAsDouble > gameManager.dashTimer + gameManager.dashCooldown )
            {
                this.transform.position += 5 * this.transform.forward;

                gameManager.dashTimer = Time.timeAsDouble;
            }
           
        }

        if (gameManager.RedArtifact)
        {
            if (Input.GetMouseButtonDown(0) && gameManager.Magazine > 0)
            {
                GameObject newBullet = Instantiate(bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation) as GameObject;
                Rigidbody bulletRB = newBullet.GetComponent<Rigidbody>();
                bulletRB.velocity = this.transform.forward * bulletSpeed;
                gameManager.Magazine--;
            }

            if (Time.timeAsDouble >= gameManager.reloadTimer + gameManager.reloadTime && gameManager.Magazine == 0)
            {
                gameManager.Magazine = 8;
            }
        }
        _rb.AddForce(-Vector3.up * gravityVel * Time.deltaTime, ForceMode.Impulse);

        camInput = Input.GetAxis("Horizontal") * rotateSpeed;
      
        Vector3 rotation = Vector3.up * camInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);
    }


    void FixedUpdate()
    {
        _rb.MovePosition(this.transform.position + input.z * this.transform.forward * moveSpeed * Time.fixedDeltaTime); 
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }
}
