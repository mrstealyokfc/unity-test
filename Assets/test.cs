using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject hookEndBase;
    private GameObject hookEndInst = null;
    void Start()
    {
        hookEndInst = Instantiate(hookEndBase, gameObject.transform.position,
            Quaternion.identity, gameObject.transform);
        hookEndInst.transform.localScale = new Vector3(10, 10, 10);
    }
    
    void Update()
    {
        hookEndInst.transform.position += new Vector3(0.01f, 0, 0);
    }
}
