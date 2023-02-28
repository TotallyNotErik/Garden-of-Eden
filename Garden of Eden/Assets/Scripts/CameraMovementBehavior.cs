using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementBehavior : MonoBehaviour
{
    private Rigidbody _rb;
    private float camInput;
    public float rotateSpeed = 1f;
    public GameManagerBehavior gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        camInput = -Input.GetAxis("Vertical") * gameManager.sensitivity *rotateSpeed * Time.deltaTime;
        Vector3 rotation = Vector3.right * camInput;
        if((transform.localRotation * Quaternion.Euler(rotation)).x > -.40 && (transform.localRotation * Quaternion.Euler(rotation)).x < 0.27)
            transform.localRotation = transform.localRotation * Quaternion.Euler(rotation);
    }
}
