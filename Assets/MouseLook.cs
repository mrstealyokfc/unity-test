using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private Transform camera=null;

    public float xSens;
    public float ySens;

    private float xRot=0;
    private float yRot=0;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        foreach (Transform obj in gameObject.transform)
            if (obj.gameObject.CompareTag("MainCamera"))
                camera = obj;
        if (camera == null)
            throw new ArgumentException("no camera found in children");
    }

    void Update()
    {
        float xInput = Input.GetAxis("Mouse X") * xSens;
        float yInput = Input.GetAxis("Mouse Y") * ySens;

        xRot -= yInput;
        yRot += xInput;

        xRot = Mathf.Clamp(xRot, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(0,yRot,0);
        camera.rotation = Quaternion.Euler(xRot,yRot,0);
        
    }
}
