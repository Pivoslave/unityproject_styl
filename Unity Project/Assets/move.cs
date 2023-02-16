using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class move : MonoBehaviour
{
    Transform camera;
    float multiplier;
    float mouseSens;
    bool isCrouching;
    bool isWalking;
    public float SprintTime = 5;
    private float rotX, rotY; 
    Vector3 relativecamdist; //camera distance relative to capsule

    int m = 1;


    float intensity = 0.4f; // Скорость раскачки
    float amplitude = 0.2f; // Высота раскачки

    Vector3 nextSwayVector;
    Vector3 nextSwayPosition;
    Vector3 startLocalPosition;

    // Start is called before the first frame update
    void Start()
    {
        camera= this.transform;
        multiplier = 0.007f;
        mouseSens = 1000f;

        isCrouching = false;
        isWalking = false;

        Vector3 rot = camera.transform.localEulerAngles; rotX = rot.x; rotY = rot.y;
        relativecamdist = this.transform.parent.position - this.transform.position;


        nextSwayVector = Vector3.up * amplitude;
        nextSwayPosition = transform.localPosition + nextSwayVector;
        startLocalPosition = transform.localPosition;
        //StartCoroutine(camTilt());
    }

    // Update is called once per frame
    void Update()
    {
        // Running
        if (Input.GetKeyDown(KeyCode.LeftShift)) { multiplier *= 2.3f; intensity += 0.1f; amplitude += 0.07f; }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) {multiplier = 0.007f; intensity -= 0.1f; amplitude -= 0.07f; }

        //Walking
        if (Input.GetKey(KeyCode.W)){ 
            if(!isCrouching) this.transform.parent.Translate(Vector3.forward * multiplier); 
            else this.transform.parent.Translate(Vector3.forward * multiplier * 0.6f);
            
            intensity = 0.4f;
            amplitude = 0.2f;
            
            isWalking = true;
        }   
        
        else if (Input.GetKey(KeyCode.S)){ 
           if(!isCrouching) this.transform.parent.Translate(Vector3.forward * (-multiplier) * 0.4f); 
            else this.transform.parent.Translate(Vector3.forward * (-multiplier) * 0.4f * 0.6f);

            amplitude = 0.08f;
            intensity = 0.25f;

            isWalking = true; }

        if (Input.GetKey(KeyCode.D)){ 
            if (!isCrouching) this.transform.parent.Translate(Vector3.right * (multiplier) * 0.5f);
            else this.transform.parent.Translate(Vector3.right * (multiplier) * 0.5f * 0.6f);

            amplitude = 0.12f;
            intensity = 0.33f;

            isWalking = true; }

        else if (Input.GetKey(KeyCode.A)){ 
            if(!isCrouching) this.transform.parent.Translate(Vector3.right * (-multiplier) * 0.5f);
            else this.transform.parent.Translate(Vector3.right * (-multiplier) * 0.5f * 0.6f);

            amplitude = 0.12f;
            intensity = 0.33f;

            isWalking = true; }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) { isWalking = false; } 

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

        if (isWalking) // Если игрок движется
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextSwayPosition, intensity * Time.deltaTime);

            if (Vector3.SqrMagnitude(transform.localPosition - nextSwayPosition) < 0.01f)
            {
                nextSwayVector = -nextSwayVector;

                nextSwayPosition = startLocalPosition + nextSwayVector;
            }
        }
        else
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, startLocalPosition, intensity * Time.deltaTime);
    }

    IEnumerator camTilt()
    {
        int m = 1;

        

        while (true)
        {
            for (int i = 0; i <= 40; i++)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position + this.transform.right * 0.2f * m, 0.0025f * i);

                yield return new WaitForSecondsRealtime(0.03f);
            }

            m *= -1;

            for (int i = 0; i <= 80; i++)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position + this.transform.right * 0.2f * m, 0.0025f * i);

                yield return new WaitForSecondsRealtime(0.03f);
            }

            m *= -1;
        }
    }


  
}
