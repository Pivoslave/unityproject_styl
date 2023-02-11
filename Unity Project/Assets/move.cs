using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    Transform camera;
    float multiplier;
    float mouseSens;

    private float rotX, rotY;

    // Start is called before the first frame update
    void Start()
    {
        camera= this.transform;
        multiplier = 0.007f;
        mouseSens = 1000f;

        Vector3 rot = camera.transform.localEulerAngles; rotX = rot.x; rotY = rot.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            camera.Translate(camera.forward * multiplier);
        }   
        
        else if (Input.GetKey(KeyCode.S))
        {
            camera.Translate(camera.forward * (-multiplier));
        }

        if (Input.GetKey(KeyCode.D))
        {
            camera.Translate(camera.right * (multiplier));
        }

        else if (Input.GetKey(KeyCode.A))
        {
            camera.Translate(camera.right * (-multiplier));
        }

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        rotX += mouseX * mouseSens * Time.deltaTime;
        rotY += mouseY * mouseSens * Time.deltaTime;

        Quaternion localRotation = Quaternion.Euler(-rotY, rotX, 0);
        camera.rotation = localRotation;
    }


}
