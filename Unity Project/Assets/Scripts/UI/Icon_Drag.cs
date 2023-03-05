using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;


// цей скрипт керує переміщенням спрайту предмета та посилає дані про нього до контролеру пересування
public class Icon_Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    GameObject ghost;
    bool isDragging;
    Drag_Controller d_control;
    Vector2 first_anchor;

    public GameObject firstcell;

    public void OnBeginDrag(PointerEventData eventData)
    {
        this.GetComponent<GraphicRaycaster>().enabled = false;
        foreach (Transform cell in GameObject.Find("Inventory_Cells").transform) if (cell.GetComponent<GraphicRaycaster>() != null) cell.GetComponent<GraphicRaycaster>().enabled = true;

        xy firstcellcoord = GameObject.Find("Items").GetComponent<ItemController>().FindByGameObject(this.gameObject).GetFirstCell();

        foreach (Transform cell in GameObject.Find("Inventory_Cells").transform) if (cell.GetComponent<InventoryCell>() != null && cell.GetComponent<InventoryCell>().posx == firstcellcoord.GetX() && cell.GetComponent<InventoryCell>().posy == firstcellcoord.GetY()) firstcell = cell.gameObject;

        GameObject.Find("Inventory_Cells").GetComponent<LogicArrayInv>().ClearSpaceWhenDragging(this.gameObject);

        this.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        isDragging= true;

        first_anchor = this.GetComponent<RectTransform>().anchoredPosition;

        d_control.drag_began = true;
        d_control.object_dragged = this.gameObject;

        
        Debug.Log("Drag Started");
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (d_control.GetState() && Time.timeScale != 1)
        {
            GameObject.Find("Inventory_Cells").GetComponent<LogicArrayInv>().OccupySpaceWhenDropped(this.gameObject, d_control.cell_selected);
            GameObject.Find("Items").GetComponent<ItemController>().ChangePlace(this.gameObject, d_control.cell_selected);
            this.GetComponent<RectTransform>().position = d_control.cell_selected.GetComponent<RectTransform>().position;
        }
        else
        {
            GameObject.Find("Inventory_Cells").GetComponent<LogicArrayInv>().OccupySpaceWhenDropped(this.gameObject, firstcell);
            this.GetComponent<RectTransform>().anchoredPosition = first_anchor;
        }

        this.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        isDragging = false;
        d_control.drag_began = false;
        this.GetComponent<GraphicRaycaster>().enabled = true;
        foreach (Transform cell in GameObject.Find("Inventory_Cells").transform) if (cell.GetComponent<GraphicRaycaster>() != null) cell.GetComponent<GraphicRaycaster>().enabled = false;
    }

    private void Update()
    {
        //if (isDragging) this.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
    }

    private void Start()
    {
        
    }

    private void Awake()
    {
        GameObject items = GameObject.Find("Items");
        d_control = items.GetComponent<Drag_Controller>();
    }
}
