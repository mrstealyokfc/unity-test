using System.ComponentModel.Design;
using UnityEngine;

public class Hook : MonoBehaviour
{

    public float range=1000;
    public GameObject hookEnd;
    public GameObject hookMiddle;
    public Transform yAim;
    
    private Transform _hookEnd;

    private string hookState="held";
    
    
    void Start()
    {
        Instantiate(hookEnd,gameObject.transform.position,
            Quaternion.identity,gameObject.transform);
        Debug.Log(GetMidpoint(new Vector3(0f,0f,0f),new Vector3(10,10,10)));
    }
    
    void Update()
    {
        if(hookState.Equals("held"))
            hookEnd.transform.rotation.Set();
    }

    Vector3 GetMidpoint(Vector3 pos1, Vector3 pos2)
    {
        return new Vector3(
            (pos1.x+pos2.x)/2f,
            (pos1.y+pos2.y)/2f,
            (pos1.z+pos2.z)/2f
            );
    }
    
}
