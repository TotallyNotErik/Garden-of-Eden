using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlayerMovement : MonoBehaviour
{
    public Rigidbody _rb;
    public float gravityVel = 9.8f;
    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position += (Vector3.up * gravityVel * .033f);
    }
}
