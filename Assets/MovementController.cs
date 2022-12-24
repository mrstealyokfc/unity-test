using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float jumpHeight;
    public float drag;
    public float playerGravity;
    public bool useGravity = true;
    
    private Vector3 velocity;
    private CharacterController _controller;
    void Start()
    {
        _controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x *= drag;
        velocity.z *= drag;
        if (!_controller.isGrounded)
            velocity += playerGravity*Vector3.down;
        else
            velocity.y = 0;

        if (Input.GetKeyDown("space"))
            velocity.y += jumpHeight;
        if (Input.GetKeyDown("l"))
            Debug.Log(velocity);
        
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 inputMovement =  (transform.forward*zInput + transform.right*xInput)*speed;
        _controller.Move((inputMovement+velocity)*Time.deltaTime);
    }
}
