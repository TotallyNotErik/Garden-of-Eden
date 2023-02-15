using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementBehavior : MonoBehaviour
{
    private Rigidbody _rb;
    private float camInput;
    public float rotateSpeed = .25f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        camInput = Input.GetAxis("Vertical") * rotateSpeed;
        Vector3 rotation = Vector3.right * camInput;

        transform.Rotate(rotation * rotateSpeed);
    }
}
