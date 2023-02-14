using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class climb : MonoBehaviour
{
    public bool over, head, body, legs;
    public bool canJump;
    
    
    // Start is called before the first frame update
    void Start()
    {
        canJump = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {

            if (!over && head && body && legs)
            {
                Physics.Raycast(this.transform.position, this.transform.forward, out RaycastHit rh, 0.7f);
                Physics.Raycast(new Vector3(rh.point.x, this.transform.position.y + 1.8f, rh.point.z), Vector3.up * -1, out RaycastHit rh2);


                Debug.Log("Initial raycast hitpos: " + rh.point + "\nCliff to climb: " + rh2.point);


                StartCoroutine(climber(this.transform.parent.position, rh2.point + new Vector3(0, this.transform.parent.transform.localScale.y, 0)));

                //this.transform.parent.position = rh2.point + new Vector3(0, this.transform.parent.transform.localScale.y, 0);
            }
            else if (canJump)
            {
                this.transform.parent.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * 5, ForceMode.Impulse);
                StartCoroutine(jumper());
            }
        }
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward * 0.7f, Color.magenta);
        Debug.DrawRay(this.transform.position + new Vector3(0, 0.7f, 0), this.transform.forward * 0.7f, Color.red);
        Debug.DrawRay(this.transform.position + new Vector3(0, 1.9f, 0), this.transform.forward * 0.8f, Color.yellow);
        Debug.DrawRay(this.transform.position - new Vector3(0, 0.7f, 0), this.transform.forward * 0.7f, Color.cyan);


        over = Physics.Raycast(this.transform.position + new Vector3(0, 1.9f, 0), this.transform.forward, 0.8f);
        head = Physics.Raycast(this.transform.position + new Vector3(0, 0.7f, 0), this.transform.forward, 0.7f);
        body = Physics.Raycast(this.transform.position, this.transform.forward, 0.7f);
        legs = Physics.Raycast(this.transform.position - new Vector3(0, 0.7f, 0), this.transform.forward, 0.7f);


        
    }

    IEnumerator climber(Vector3 pos, Vector3 dest)  
    {
        this.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        for(int i = 1; i<= 40; i++)
        {
            this.transform.parent.position = Vector3.Lerp(pos, dest, 0.025f * i);
            yield return new WaitForSeconds(0.02f);
        }

        this.transform.parent.gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    IEnumerator jumper()
    {
        canJump = false;
        yield return new WaitForSecondsRealtime(1.1f);
        canJump= true;
    }
}
