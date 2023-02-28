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
        if(transform.eulerAngles.x >= 360-45 || transform.eulerAngles.x <= 30)
            transform.localRotation = transform.localRotation * Quaternion.Euler(rotation);
        else if(transform.eulerAngles.x < 360-45 && transform.eulerAngles.x > 180  )
            transform.localRotation = Quaternion.Euler(360-45,0,0);
        else if(transform.eulerAngles.x >= 30 && transform.eulerAngles.x < 180)
            transform.localRotation = Quaternion.Euler(30,0,0);
    }
}
