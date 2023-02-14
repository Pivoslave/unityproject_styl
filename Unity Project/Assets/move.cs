using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    Transform camera;
    float multiplier;
    float mouseSens;

    bool isCrouching;

    public float SprintTime = 5;

    private float rotX, rotY;

    // Start is called before the first frame update
    void Start()
    {
        camera= this.transform;
        multiplier = 0.007f;
        mouseSens = 1000f;

        isCrouching = false;

        Vector3 rot = camera.transform.localEulerAngles; rotX = rot.x; rotY = rot.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Running
        if (Input.GetKeyDown(KeyCode.LeftShift)) multiplier *= 2.3f;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) multiplier = 0.007f;

        //Walking
        if (Input.GetKey(KeyCode.W)){ this.transform.parent.Translate(Vector3.forward * multiplier); }   
        
        else if (Input.GetKey(KeyCode.S)){ this.transform.parent.Translate(Vector3.forward * (-multiplier)); }

        if (Input.GetKey(KeyCode.D)){ this.transform.parent.Translate(Vector3.right * (multiplier)); }

        else if (Input.GetKey(KeyCode.A)){ this.transform.parent.Translate(Vector3.right * (-multiplier)); }


        // Rotation
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotX += mouseX * mouseSens * Time.deltaTime;
        rotY += mouseY * mouseSens * Time.deltaTime;
        this.transform.parent.rotation = Quaternion.Euler(this.transform.parent.rotation.x, rotX, this.transform.parent.rotation.z);
        rotY = Mathf.Clamp(rotY, -90, 80);

        Quaternion localRotation = Quaternion.Euler(-rotY, rotX, 0);
        camera.rotation = localRotation;

        // Sprint Timer
        if (multiplier != 0.007f) SprintTime -= Time.deltaTime;
        else if (SprintTime < 5) SprintTime += Time.deltaTime * 0.6f;
        else if (SprintTime > 5) SprintTime = 5;

        if (multiplier != 0.007f && SprintTime <= 0) multiplier = 0.007f;

        //Crouch

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            this.transform.parent.localScale = new Vector3(1, 0.5f, 1);
            isCrouching = true;
        }

        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            this.transform.parent.localScale = Vector3.one;
            isCrouching = false;
        }

        if (Input.GetKeyDown(KeyCode.C)) {
            if (isCrouching) this.transform.parent.localScale = Vector3.one;
            else this.transform.parent.localScale = new Vector3(1, 0.5f, 1);

            isCrouching = !isCrouching;
        } 
    }


}
