using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movemint : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float jumpHeight;
    public float drag;
    
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
        //velocity += Physics.gravity;

        if (Input.GetKeyDown("space"))
            velocity.y += jumpHeight;
        
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        Vector3 inputMovement =  (transform.forward*zInput + transform.right*xInput)*speed;
        _controller.Move((inputMovement+velocity)*Time.deltaTime);
    }
}
