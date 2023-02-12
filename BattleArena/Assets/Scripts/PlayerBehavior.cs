using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float jumpVelocity = 5f;
    public float moveSpeed = 10f;
 	public float rotateSpeed = .25f;
    public float gravityVel = 9.8f;
    public float distanceToGround = 0.1f;

    public LayerMask groundLayer;

    private Vector3 input;
    private float camInput;

	private Rigidbody _rb;
    private CapsuleCollider _col;


    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
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

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rb.AddForce(Vector3.up * jumpVelocity, ForceMode.Impulse);
        }
        /*
        if (Input.GetKey(KeyCode.A))
        {
            input.x = -0.5f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
           input.x = 0.5f;
        }
        else { input.x = 0f; }
        */
        camInput = Input.GetAxis("Horizontal") * rotateSpeed;
        _rb.AddForce(-Vector3.up * gravityVel * +Time.deltaTime, ForceMode.Impulse);
        Vector3 rotation = Vector3.up * camInput;

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        _rb.MoveRotation(_rb.rotation * angleRot);
        
    }


    void FixedUpdate()
    {

        _rb.MovePosition(this.transform.position + input.z * this.transform.forward * moveSpeed * Time.fixedDeltaTime); 

        /*_rb.MovePosition(this.transform.position + this.transform.right * hInput * Time.fixedDeltaTime);*/
    }

    private bool IsGrounded()
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z);

        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, distanceToGround, groundLayer, QueryTriggerInteraction.Ignore);
        return grounded;
    }
}
