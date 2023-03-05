using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


// звідси беруться дані про переносимий спрайт та комірку на якій розташований курсор
public class Drag_Controller : MonoBehaviour
{
    public bool drag_began;
    public GameObject object_dragged;
    public GameObject cell_selected;

    
    public bool GetState()
    {
        return GameObject.Find("Inventory_Cells").GetComponent<LogicArrayInv>().CheckCellsByOne(object_dragged, cell_selected);
    }
}
