using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class generalControl : MonoBehaviour
{
    bool MenuOpen = false;

    Transform cells;
    Transform static_ui;
    Transform items;

    // Start is called before the first frame update
    void Start()
    {
        cells = GameObject.Find("Inventory_Cells").transform;
        static_ui = GameObject.Find("Static_UI").transform;
        items = GameObject.Find("Items").transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MenuOpen = !MenuOpen;

            //cells.GetComponent<Image>().enabled = MenuOpen;
            foreach(Transform cell in cells) if(cell.GetComponent<Image>() != null) cell.GetComponent<Image>().enabled = MenuOpen;
            foreach(Transform item in static_ui) if(item.name != "Crosshair") item.GetComponent<Image>().enabled = MenuOpen;
            foreach(Transform sprite in items) if (sprite.GetComponent<Image>() != null) sprite.GetComponent<Image>().enabled = MenuOpen;
            Time.timeScale = MenuOpen ? 0 : 1;
            Cursor.visible = MenuOpen;
            
            Cursor.lockState = MenuOpen ? CursorLockMode.None : CursorLockMode.Locked;  
        }

        
    }

    
}
