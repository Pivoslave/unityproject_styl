using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grab : MonoBehaviour
{
    GameObject toCarry;
    bool carried;

    // Start is called before the first frame update
    void Start()
    {
        carried = false;
        toCarry = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!carried)
            {

                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit rH, 1.7f))
                {
                    if (rH.collider.gameObject.tag == "Interactable")
                    {
                        carried = true;
                        toCarry = rH.collider.gameObject;
                        if (toCarry.GetComponent<Rigidbody>() != null) toCarry.GetComponent<Rigidbody>().isKinematic = true;
                        if (toCarry.GetComponent<MeshCollider>() != null) toCarry.GetComponent<MeshCollider>().enabled = false;
                    }

                    else if(rH.collider.gameObject.tag == "Collectable")
                    {
                        GameObject.Destroy(rH.collider.gameObject);
                    }
                };
            }


            else
            {
                carried = false;
                if (toCarry.GetComponent<Rigidbody>() != null) toCarry.GetComponent<Rigidbody>().isKinematic = false;
                if (toCarry.GetComponent<MeshCollider>() != null) toCarry.GetComponent<MeshCollider>().enabled = true;
            }
        }

        if (Input.GetMouseButtonDown(0) && carried)
        {
            carried = false;
            if (toCarry.GetComponent<Rigidbody>() != null)
            {
                toCarry.GetComponent<Rigidbody>().isKinematic = false;
                toCarry.GetComponent<Rigidbody>().AddForce(this.transform.forward * 10, ForceMode.Impulse);
            }
                if (toCarry.GetComponent<MeshCollider>() != null) toCarry.GetComponent<MeshCollider>().enabled = true;
        }


        if (carried)
        {
            toCarry.transform.position = this.transform.position + this.transform.forward;
            toCarry.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        else { }

        Debug.DrawRay(transform.position, transform.forward * 1.7f, Color.cyan);
    }


}
