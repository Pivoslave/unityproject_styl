using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public struct inventoryItem
{
    public string itemname;
    public string imagepath;
    public int x, y;

    public inventoryItem(string _name, string _path, int _x, int _y)
    {
        itemname = _name; // назва предмета
        imagepath = _path; // шл€х до спрайту у папц≥ "Resources"
        x = _x; // ск≥льки м≥сц€ в≥дносно 0 займаЇ по х (e.g х = 1 => 2 слоти по х (1 + х))
        y = _y; // ск≥льки м≥сц€ в≥дносно 0 займаЇ по у (e.g у = 2 => 3 слоти по у (1 + у))
    }
}

public class ItemDescriptor : MonoBehaviour
{

    public inventoryItem StructByGameObject(GameObject a)
    {
        switch (a.gameObject.name)
        {
            case string name when name.Contains("Bread"): return new inventoryItem("bread", "Sprites/bread", 1, 0);
            case string name when name.Contains("BigCheese"): return new inventoryItem("big_cheese", "Sprites/big_cheese", 1, 1);
            case string name when name.Contains("Cheese"): return new inventoryItem("cheese", "Sprites/cheese", 0, 0);





            default: return new inventoryItem("", "", -1, -1);
        }
    }
}
