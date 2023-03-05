using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// цей скрипт посилає дані про комірки до контролера
public class Cell_to_Drag : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Drag_Controller d_controller;
    GameObject cell_highlight;
    IEnumerator colorchanger;
    bool selected;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("entered cell");
        d_controller.cell_selected = this.gameObject;

        

        cell_highlight = GameObject.Instantiate(this.gameObject, GameObject.Find("Inventory_Cells").transform);
        Destroy(cell_highlight.GetComponent<GraphicRaycaster>());
        Destroy(cell_highlight.GetComponent<Cell_to_Drag>());
        Destroy(cell_highlight.GetComponent<InventoryCell>());
        cell_highlight.GetComponent<Image>().sprite = null;

        bool available = d_controller.GetState();
        if (available) Debug.Log("cells available");
        else Debug.Log("cells unavailable");

        cell_highlight.GetComponent<Image>().color = available ? new Color(0.7f, 1, 0) : new Color(0.54510f, 0, 0);


        colorchanger = alphaChange(cell_highlight);
        StartCoroutine(colorchanger);
    }

    private void Start()
    {
        d_controller = GameObject.Find("Items").GetComponent<Drag_Controller>();
    }

    private void FixedUpdate()
    {
        if (this.GetComponent<GraphicRaycaster>().enabled && selected) GameObject.Find("Items").GetComponent<Drag_Controller>().cell_selected = this.gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        selected = false;
        StopCoroutine(colorchanger);
        Destroy(cell_highlight.gameObject);
    }

    IEnumerator alphaChange(GameObject cell)
    {
        //float alpha = 0;
        bool returner = false;
        
        Color color = cell.GetComponent<Image>().color;


        while (true)
        {
            switch (returner)
            {
                case false:

                    for (float i = 0; i <= 0.7f; i += 0.01f)
                    {
                        color = new Color(color.r, color.g, color.b, i);

                        cell.GetComponent<Image>().color = color;

                        yield return new WaitForSecondsRealtime(0.01f);
                    }

                    break;

                case true:

                    for (float i = 0.7f; i >= 0; i -= 0.01f)
                    {
                        color = new Color(color.r, color.g, color.b, i);

                        cell.GetComponent<Image>().color = color;

                        yield return new WaitForSecondsRealtime(0.01f);
                    }

                    break;
            }

            returner = !returner;
        }
        
    } 
}
