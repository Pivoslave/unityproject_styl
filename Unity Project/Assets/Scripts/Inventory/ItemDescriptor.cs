using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescriptor : MonoBehaviour
{
   public struct inventoryItem
    {
        public string itemname;
        public string imagepath;
        public int x, y;

        public inventoryItem(string _name, string _path, int _x, int _y) { 
            itemname= _name;
            imagepath= _path;
            x= _x;
            y= _y;
        }
    }

    public inventoryItem bread = new inventoryItem("bread", "Sprites/bread", 1, 1);
}
