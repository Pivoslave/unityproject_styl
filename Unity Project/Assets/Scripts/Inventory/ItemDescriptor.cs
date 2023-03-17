using System;
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

public enum itemType
{
    Food,
    Weapon,
    Offhand,
    Quickuse,

    err
}

public class ItemDescriptor : MonoBehaviour
{

    public inventoryItem StructByGameObject(GameObject a)
    {
        switch (a.name)
        {
            case string name when name.Contains("Bread"): return new inventoryItem("bread", "Sprites/bread", 1, 0);
            case string name when name.Contains("BigCheese"): return new inventoryItem("big_cheese", "Sprites/big_cheese", 1, 1);
            case string name when name.Contains("Cheese"): return new inventoryItem("cheese", "Sprites/cheese", 0, 0);
            case string name when name.Contains("AshPoker"): return new inventoryItem("ash_poker", "Sprites/ash_poker", 5, 0);
            case string name when name.Contains("PipeWrench"): return new inventoryItem("pipe_wrench", "Sprites/pipe_wrench", 0, 2);
            case string name when name.Contains("Opium"): return new inventoryItem("opium", "Sprites/opium", 0, 0);
            case string name when name.Contains("Whisky"): return new inventoryItem("whisky", "Sprites/whisky", 0, 1);


            default: return new inventoryItem("", "", -1, -1);
        }
    }

    public inventoryItem StructByName(string item_name)
    {
        switch (item_name)
        {
            case string name when name.Contains("bread") : return new inventoryItem("bread", "Sprites/bread", 1, 0);
            case string name when name.Contains("big_cheese"): return new inventoryItem("big_cheese", "Sprites/big_cheese", 1, 1);
            case string name when name.Contains("cheese"): return new inventoryItem("cheese", "Sprites/cheese", 0, 0);
            case string name when name.Contains("ash_poker"): return new inventoryItem("ash_poker", "Sprites/ash_poker", 5, 0);
            case string name when name.Contains("pipe_wrench"): return new inventoryItem("pipe_wrench", "Sprites/pipe_wrench", 0, 2);
            case string name when name.Contains("opium"): return new inventoryItem("opium", "Sprites/opium", 0, 0);
            case string name when name.Contains("whisky"): return new inventoryItem("whisky", "Sprites/whisky", 0, 1);

            default: return new inventoryItem("", "", -1, -1);
        }
    }

    public itemType getTypeByName(string item_name)
    {
        switch (item_name)
        {
            case string name when (name.Contains("bread") || name.Contains("big_cheese") || name.Contains("cheese")): return itemType.Food;
            case string name when (name.Contains("ash_poker") || name.Contains("pipe_wrench")): return itemType.Weapon;
            case string name when (name.Contains("opium") || name.Contains("whisky")): return itemType.Quickuse;

            default: return itemType.err;
        }
    }

    public GameObject getDroppable(GameObject a)
    {
        switch (a.name)
        {
            case string name when name.Contains("bread"): return Resources.Load<GameObject>("Prefabs/Bread");
            case string name when name.Contains("big_cheese"): return Resources.Load<GameObject>("Prefabs/BigCheese");
            case string name when name.Contains("cheese"): return Resources.Load<GameObject>("Prefabs/Cheese");
            case string name when name.Contains("ash_poker"): return Resources.Load<GameObject>("Prefabs/AshPoker");
            case string name when name.Contains("pipe_wrench"): return Resources.Load<GameObject>("Prefabs/PipeWrench");
            case string name when name.Contains("opium"): return Resources.Load<GameObject>("Prefabs/Opium");
            case string name when name.Contains("whisky"): return Resources.Load<GameObject>("Prefabs/Whisky");

            default: return null;
        }
    }

    public string getNameByInvItem(GameObject a)
    {
        switch (a.name)
        {
            case string name when name.Contains("bread"): return "Bread";
            case string name when name.Contains("big_cheese"): return "BigCheese";
            case string name when name.Contains("cheese"): return "Cheese";
            case string name when name.Contains("ash_poker"): return "AshPoker";
            case string name when name.Contains("pipe_wrench"): return "PipeWrench";
            case string name when name.Contains("opium"): return "Opium";
            case string name when name.Contains("whisky"): return "Whisky";

            default: return null;
        }
    }
}
