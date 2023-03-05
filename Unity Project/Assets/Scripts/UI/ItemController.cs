using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.UIElements;

public struct xy
{
    int x, y;

    public xy(int _x, int _y) { x = _x; y = _y; }
    public int GetX() { return x; }
    public int GetY() { return y; }

    public void UnpackStruct(xy str, int @x, int @y){
        @x = str.x;
        @y = str.y;
    }
}

public struct item_location
{
    public GameObject item;

    int x_first, y_first;
    int x_last, y_last;

    public item_location(GameObject g_o, int x1, int y1, int x2, int y2)
    {
        item = g_o;
        x_first = x1;
        y_first = y1;
        x_last = x2;
        y_last = y2;
    }

    public item_location(GameObject g_o, xy firscell, xy lastcell)
    {
        item = g_o;
        x_first = firscell.GetX();
        y_first = firscell.GetY();
        x_last = lastcell.GetX();
        y_last = lastcell.GetY();
    }

    public xy GetFirstCell() { return new xy(x_first, y_first); }
    public xy GetLastCell() { return new xy(x_last, y_last); }
}

public class ItemController : MonoBehaviour
{


    [SerializeField] List<item_location> items = new List<item_location>();

    // додати елемент до списку
    public void Add(item_location a)
    {
        items.Add(a);

        Debug.Log(a.item.name + " at (" + a.GetFirstCell().GetX() + ";" + a.GetFirstCell().GetY() + "), (" + a.GetLastCell().GetX() + ";" + a.GetLastCell().GetY() + ")");
    }

    // виключити елемент зі списку
    public void Remove(item_location a)
    {
        items.Remove(a);
    }

    public void Remove(GameObject a)
    {
        item_location toremove = new item_location(null, -1, -1, -1, -1);
        foreach (item_location il in items) if (il.item == a) toremove = il;
        items.Remove(toremove); 
    }

    public void ChangePlace(GameObject a, GameObject selectedCell)
    {
        item_location toremove = new item_location(null, -1, -1, -1, -1);
        foreach (item_location il in items) if (il.item == a) toremove = il;

        items.Remove(toremove);


        inventoryItem item = GameObject.Find("Items").GetComponent<ItemDescriptor>().StructByName(a.name);
        InventoryCell incell = selectedCell.GetComponent<InventoryCell>();

        int x = a.GetComponent<RectTransform>().pivot == Vector2.zero ? item.y : item.x;
        int y = a.GetComponent<RectTransform>().pivot == Vector2.zero ? item.x : item.y;

        items.Add(new item_location(a, incell.posx, incell.posy, incell.posx + x, incell.posy + y));
    } 


    public item_location FindByGameObject(GameObject a)
    {
        foreach (item_location il in items) if (il.item == a) return il;
        return new item_location(null, -1, -1, -1, -1);
    }

    public int getItemIndex(GameObject a)
    {
            foreach (item_location il in items)
            {
                if (il.item == a) return items.IndexOf(il);
            }

            return -1;
    }
}
