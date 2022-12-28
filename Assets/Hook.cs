using System;
using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UIElements;

public class Hook : MonoBehaviour
{

    public float range=1000;
    public GameObject hookEndBase;
    public GameObject hookMiddle;
    public Transform yAim;
    public float hookSpeed;

    private Transform hookSubject;
    private Vector3 hookSubjectOffset;
    private MovementController _movementController;
    private CharacterController _controller;
    private GameObject hookEndInst;
    private string hookState="held";
    private Rigidbody hookEndRb;
    
    void Start()
    {
        hookEndInst = Instantiate(hookEndBase,gameObject.transform.position,
            Quaternion.identity);
        Debug.Log(GetMidpoint(new Vector3(0f,0f,0f),new Vector3(10,10,10)));
        hookEndRb = hookEndInst.AddComponent<Rigidbody>();
        hookEndRb.useGravity = false;
        hookEndInst.AddComponent<HookEndScript>().callback = this;
        hookEndInst.GetComponent<Collider>().isTrigger = true;
        _controller = transform.parent.gameObject.GetComponent<CharacterController>();
        _movementController = transform.parent.gameObject.GetComponent<MovementController>();
    }
    
    void Update()
    {
        switch (hookState)
        {
            case "held":
                WaitForPlayer();
                break;
            case "extending":
                ExtendHook();
                break;
            case "hooked":
                processHooked();
                break;
            case "pulling":
                pullPlayer();
                break;
            case "retracting":
                RetractHook();
                break;
        }

        if (hookState.Equals("pulling") || hookState.Equals("hooked"))
            hookEndInst.transform.position = hookSubject.position + hookSubjectOffset;
    }

    private void pullPlayer()
    {
        _controller.transform.position = Vector3.MoveTowards(
            _controller.transform.position,
            hookEndInst.transform.position,
            hookSpeed*Time.deltaTime);
        _movementController.useGravity = false;
    }
    private void processHooked()
    {
        if (Input.GetMouseButton(0))
            hookState = "pulling";
        else if (Input.GetMouseButton(1))
        {
            pointHookAtPlayer();
            hookState = "retracting";
        }
           
    }

    private void pointHookAtPlayer()
    {

    }
    
    private void WaitForPlayer()
    {
        var _rot = gameObject.transform.rotation.eulerAngles;
        gameObject.transform.rotation = Quaternion.Euler(yAim.rotation.eulerAngles.x,_rot.y,_rot.z);
        hookEndInst.transform.position = transform.position;
        hookEndInst.transform.rotation = transform.rotation;
        if (Input.GetMouseButtonDown(0))
            hookState = "extending";

    }
    private void ExtendHook()
    {
        hookEndRb.velocity= new Vector3(0,0,0);
        hookEndRb.angularVelocity = new Vector3(0,0,0);
        hookEndInst.transform.Translate(Vector3.forward * (hookSpeed * Time.deltaTime));
        if (Input.GetMouseButton(1))
            hookState = "retracting";
        if ((hookEndInst.transform.position - transform.position).magnitude > range)
            hookState = "retracting";
    }

    private void RetractHook()
    {
        hookEndInst.transform.position = Vector3.MoveTowards(
            hookEndInst.transform.position,
            transform.position,
            hookSpeed*Time.deltaTime);
    }
    
    Vector3 GetMidpoint(Vector3 pos1, Vector3 pos2)
    {
        return new Vector3(
            (pos1.x+pos2.x)/2f,
            (pos1.y+pos2.y)/2f,
            (pos1.z+pos2.z)/2f
            );
    }

    Vector3 GetVectorFromRot(Vector3 rot)
    {
        var h = Mathf.Sin(rot.x);
        var w = Mathf.Sin(rot.y);
        return new Vector3(0, w, h);
    }

    public void onHookEndCollisionCallback(Collider other)
    {
        Debug.Log("OWO");
        if (other.gameObject.CompareTag("Player"))
        {
            if (hookState.Equals("pulling")||hookState.Equals("retracting"))
                hookState = "held";
            hookEndInst.transform.position = transform.position;
            _movementController.useGravity = true;
            return;
        }

        if (hookState.Equals("extending"))
        {
            hookState = "hooked";
            hookSubject = other.transform;
            hookSubjectOffset = hookEndInst.transform.position - hookSubject.transform.position;
        }

    }
}

internal class HookEndScript : MonoBehaviour
{
    public Hook callback;

    private void OnTriggerEnter(Collider other)
    {
        callback.onHookEndCollisionCallback(other);
    }
    

    private void Start()
    {
        Debug.Log("uwu");    
    }
}
