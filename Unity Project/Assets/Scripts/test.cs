using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit raycastHit = new RaycastHit();
        Physics.Raycast(this.transform.position, this.transform.forward * 30, out raycastHit);

        Debug.Log(raycastHit.transform);
    }
}
