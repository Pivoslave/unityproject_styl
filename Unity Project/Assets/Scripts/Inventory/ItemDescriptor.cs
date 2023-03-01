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
        itemname = _name; // ����� ��������
        imagepath = _path; // ���� �� ������� � ����� "Resources"
        x = _x; // ������ ���� ������� 0 ����� �� � (e.g � = 1 => 2 ����� �� � (1 + �))
        y = _y; // ������ ���� ������� 0 ����� �� � (e.g � = 2 => 3 ����� �� � (1 + �))
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
