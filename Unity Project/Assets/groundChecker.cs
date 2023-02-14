using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundChecker : MonoBehaviour
{
    bool onGround;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        onGround = true;    
    }

    private void OnCollisionExit(Collision collision)
    {
        onGround= false;
    }

    public bool checkGround()
    {
        return onGround;
    }
}
