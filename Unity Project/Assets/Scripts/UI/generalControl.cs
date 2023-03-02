using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class generalControl : MonoBehaviour
{
    bool MenuOpen = false;

    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MenuOpen = !MenuOpen;

            foreach (Transform cell in this.transform.Find("Inventory2")) cell.GetComponent<Image>().enabled = MenuOpen;
            this.transform.Find("Inventory2").GetComponent<Image>().enabled = MenuOpen;
            this.transform.Find("InventoryDelimiter").GetComponent<Image>().enabled = MenuOpen;

            
        }

        Time.timeScale = MenuOpen ? 0 : 1;
    }

    
}
