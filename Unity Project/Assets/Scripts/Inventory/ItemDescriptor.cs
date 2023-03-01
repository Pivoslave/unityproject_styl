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
    public inventoryItem bread = new inventoryItem("bread", "Sprites/bread", 1, 1);

    public inventoryItem StructByGameObject(GameObject a)
    {
        if (a.name.Contains("Bread")) return bread;

        switch (a.gameObject.name)
        {
            case string name when name.Contains("Bread"): return bread;
            
            default: return new inventoryItem("", "", -1, -1);
        }
    }
}
