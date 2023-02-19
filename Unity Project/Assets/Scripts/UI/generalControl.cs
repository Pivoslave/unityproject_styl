using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generalControl : MonoBehaviour
{
    bool MenuOpen;

    // Start is called before the first frame update
    void Start()
    {
        MenuOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!MenuOpen)
            {
                MenuOpen = true;
                foreach (Transform x in this.transform) if (x.gameObject.name == "Inventory") x.gameObject.SetActive(true);
            }

            else if (MenuOpen)
            {
                MenuOpen = false;
                foreach (Transform x in this.transform) if (x.gameObject.name == "Inventory") x.gameObject.SetActive(false);
            }
        }


        if (MenuOpen) Time.timeScale = 0; else Time.timeScale = 1;
    }

    
}
