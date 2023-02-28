using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public inventoryItem bread = new inventoryItem("bread", "Sprites/bread", 1, 1);
}
