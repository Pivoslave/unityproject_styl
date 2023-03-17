using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// інтегрувати як вивід ф-ції FindFirst для випадків, коли предмет повернуто на 90 градусів
public struct rect_rot
{
    public RectTransform rect;
    public bool is_rotated;

    

    public rect_rot(RectTransform rt, bool ir)
    {
        rect = rt;
        is_rotated = ir;
    }
}

public class LogicArrayInv : MonoBehaviour
{
    bool[,] occup;

    public bool[] check;

    public int xsize = 0; 
    public int ysize = 0;

    InventoryCell inventoryCell;

    public void FormArray()
    {
        foreach (Transform x in this.transform)
        {
            inventoryCell = x.GetComponent<InventoryCell>();

            if (inventoryCell != null) {
                if (inventoryCell.posx > xsize) xsize = inventoryCell.posx;
                if (inventoryCell.posy > ysize) ysize = inventoryCell.posy;
            }
        }

        occup = new bool[xsize + 1, ysize + 1];
        Debug.Log("new bool array, size: x - " + occup.GetLength(0) + ", y - " + occup.GetLength(1));

        FillArray();
    }

    public void FillArray()
    {
        foreach(Transform x in this.transform)
        {
            inventoryCell = x.GetComponent<InventoryCell>();

            if (inventoryCell != null) occup[inventoryCell.posx, inventoryCell.posy] = inventoryCell.getStatus();
        }
    }

    public rect_rot FindFirst(int x, int y){
        RectTransform rTransform;

        rTransform = FindFirstOneDim(x, y);
        if (rTransform != null) return new rect_rot(rTransform, false);

        else rTransform = FindFirstOneDim(y, x);
        if (rTransform != null) return new rect_rot(rTransform, true);

        else return new rect_rot(null, false);
    }


    // Доповнити ф-цію для повернутого предмета, змінити тип вихідних даних на структуру описану на початку скрипта
    public RectTransform FindFirstOneDim(int x, int y)
    {
        for (int i = 0; i <= xsize; i++){ // i <=> x
            for (int j = 0; j <= ysize; j++){ // j <=> y
                
                if(i + x <= xsize && j + y <= ysize){ // пошук не повинен виходити за межі комірок інвентаря
                    
                    // тут починається безпосередня перевірка комірок
                    bool cellAvailable = true; // значення комірки за замовчуванням - незайнята

                    string output = ""; // дебаґ

                    for(int i1 = 0; i1<=x; i1++){ // перевірка х0 + (0; х)
                        for(int j1 = 0; j1<=y; j1++){ // перевірка у0 + (0; у)

                            output = output + "(" + (i + i1).ToString() + (j + j1).ToString() + ")"; // дебаґ

                            if (occup[i + i1, j + j1] == true) cellAvailable = false; // якщо комірка зайнята, прапорець зміниться
                        }
                    }

                    Debug.Log(output); // дебаґ


                    if(cellAvailable) { // якщо усі перевірені комірки доступні 
                        foreach (Transform cell in this.transform) 
                            if (cell.GetComponent<InventoryCell>() != null){
                                InventoryCell incell = cell.GetComponent<InventoryCell>();
                                if(incell.posx == i && incell.posy == j) // знаходится комірка з координатами зі співпадінням у зовнішніх циклах
                                {
                                    //Debug.Log(cell.name);
                                    return cell.gameObject.GetComponent<RectTransform>(); // і повертається її Transform 
                                }
                            }
                    }
                }
            }
        }

        return null; // інакше не повертається нічого;
    }

    public void GetInfo() // отримується статус масиву  | дебаґ
    {
        for (int i = 0; i < occup.GetLength(0); i++) {
            string line = "";

            for (int j = 0; j < occup.GetLength(1); j++)
            {
                char tf = occup[i, j] == true ? '1' : '0';

                line = line + tf + " ";
            }
            Debug.Log(line + "\n");
        } 
    }

    /// <summary>
    /// Змінює статус елемента масива на true або false
    /// </summary>
    /// <param name="occupying">true - займає клітину, false - звільняє</param> 
    /// <param name="posx"> координата х лівої верхньої клітини </param>
    /// <param name="posy"> координата у лівої верхньої клітини </param>
    /// <param name="x"> зсув предмета за х відносно лівої верхньої клітини </param>
    /// <param name="y"> зсув предмета за у відносно лівої верхньої клітини </param>
    public void ChangeCellStatus(bool occupying , int posx, int posy, int x, int y)
    {
        for(int i = 0; i<=x; i++)
        {
            for(int j = 0; j<=y; j++)
            {
                Debug.Log((posx + i) + ", " + (posy + j));
            }
        }
    }

    public void ChangeCellStatus(bool occupying, int x_pos, int y_pos, int x, int y, bool rotated)
    {
        int x1 = rotated == false ? x : y;
        int y1 = rotated == false ? y : x;

        for (int i = 0; i <= x1; i++)
        {
            for (int j = 0; j <= y1; j++)
            {
                Debug.Log((x_pos + i) + ", " + (y_pos + j));

                foreach(Transform cell in this.transform) if(cell.GetComponent<InventoryCell>() != null)
                    {
                        if(cell.GetComponent<InventoryCell>().posx == x_pos + i && cell.GetComponent<InventoryCell>().posy == y_pos + j)
                        {
                            cell.GetComponent<InventoryCell>().changeStatus(occupying);
                            occup[x_pos + i, y_pos + j] = occupying;
                            break;
                        }
                    }
            }
        }

        //DrawCellByArray();
    }

    public void RefillArray()
    {
        
    }

    /// <summary>
    /// функція перевірки масиву комірок по комірці, на яку наведено курсор
    /// </summary>
    /// <param name="item_sprite"> предмет, що переноситься </param>
    /// <param name="cell"> комірка на яку наведено </param>
    /// <returns></returns>
    public bool CheckCellsByOne(GameObject item_sprite, GameObject cell)
    {
        bool cells_available = true;

        string debug = "";

        inventoryItem item_dimensions = GameObject.Find("Items").GetComponent<ItemDescriptor>().StructByName(item_sprite.name);

        check = new bool[(item_dimensions.x + 1) * (item_dimensions.y + 1)];
        

        int x = item_sprite.GetComponent<RectTransform>().pivot == Vector2.zero ? item_dimensions.y : item_dimensions.x;
        int y = item_sprite.GetComponent<RectTransform>().pivot == Vector2.zero ? item_dimensions.x : item_dimensions.y;

        int i = 0;

        foreach (Transform cell1 in GameObject.Find("Inventory_Cells").transform)
        {
            if (cell1.GetComponent<InventoryCell>() == null) continue;

            else if (cell.GetComponent<InventoryCell>().posx + x > xsize ||
                cell.GetComponent<InventoryCell>().posy + y > ysize) { Debug.Log("false"); return false; }

            else if (cell1.GetComponent<InventoryCell>().posx < cell.GetComponent<InventoryCell>().posx ||
                cell1.GetComponent<InventoryCell>().posx > cell.GetComponent<InventoryCell>().posx + x) continue;

            else if (cell1.GetComponent<InventoryCell>().posy < cell.GetComponent<InventoryCell>().posy ||
                cell1.GetComponent<InventoryCell>().posy > cell.GetComponent<InventoryCell>().posy + y) continue;

            else if (cell1.GetComponent<InventoryCell>().getStatus() == true) return false;
        }

        Debug.Log(debug);

        

        return true;

        //if (cell1.GetComponent<InventoryCell>() == null) continue;
        //else if (cell1.GetComponent<InventoryCell>().posx + x >= xsize ||
        //    cell1.GetComponent<InventoryCell>().posy + y >= ysize) continue;

        //else if (!(cell1.GetComponent<InventoryCell>().posx >= cell.GetComponent<InventoryCell>().posx) ||
        //    !(cell1.GetComponent<InventoryCell>().posx <= cell.GetComponent<InventoryCell>().posx + x)) continue;

        //else if (cell1.GetComponent<InventoryCell>().posx >= cell.GetComponent<InventoryCell>().posx &&
        //    cell1.GetComponent<InventoryCell>().posx <= cell.GetComponent<InventoryCell>().posx + x &&
        //    cell1.GetComponent<InventoryCell>().posy >= cell.GetComponent<InventoryCell>().posy &&
        //    cell1.GetComponent<InventoryCell>().posy <= cell.GetComponent<InventoryCell>().posx + y &&
        //    cell1.GetComponent<InventoryCell>().getStatus() == true)
        //{
        //    Debug.Log("no");
        //    return false;
        //}
    }

    public void ClearSpaceWhenDragging(GameObject a)
    {
        item_location location = GameObject.Find("Items").GetComponent<ItemController>().FindByGameObject(a);

        //MakeAllCellsVisible();

        foreach(Transform cell in GameObject.Find("Inventory_Cells").transform)
        {
            if (cell.GetComponent<InventoryCell>() == null) continue;
            else if (isBetween(cell.GetComponent<InventoryCell>().posx, location.GetFirstCell().GetX(), location.GetLastCell().GetX(), true) &&
                isBetween(cell.GetComponent<InventoryCell>().posy, location.GetFirstCell().GetY(), location.GetLastCell().GetY(), true)) cell.GetComponent<InventoryCell>().changeStatus(false);
        }

        
    }

    // дописати
    public void OccupySpaceWhenDropped(GameObject a, GameObject chosen)
    {
        inventoryItem item = GameObject.Find("Items").GetComponent<ItemDescriptor>().StructByName(a.name);

        int x = a.GetComponent<RectTransform>().pivot == Vector2.zero ? item.y : item.x;
        int y = a.GetComponent<RectTransform>().pivot == Vector2.zero ? item.x : item.y;

        InventoryCell chosenCell = chosen.GetComponent<InventoryCell>();

        foreach (Transform cell in GameObject.Find("Inventory_Cells").transform) if (cell.GetComponent<InventoryCell>() != null)
            {
                InventoryCell incell = cell.GetComponent<InventoryCell>();

                if (incell == null) continue;
                if(isBetween(incell.posx, chosenCell.posx, chosenCell.posx + x, true) && isBetween(incell.posy, chosenCell.posy, chosenCell.posy + y, true)) incell.changeStatus(true);
            }

        //DrawCellByArray();
    }

    public bool isBetween (int variable, int lower, int bigger, bool including)
    {
        return including ? ((variable >= lower) && (variable <= bigger)) : ((variable > lower) && (variable < bigger));
    }
    
    public void DrawCellByArray()
    {
        foreach (Transform cell in GameObject.Find("Inventory_Cells").transform)
        {
            if (cell.name.Contains("image") && !cell.name.Contains("Clone"))
            {
                string[] divided = cell.name.Split('_');
                int x = Convert.ToInt32(divided[1]);
                int y = Convert.ToInt32(divided[2]);

                if (occup[x, y] == true) cell.GetComponent<Image>().color = new Color(1, 1, 1, 0);
                else cell.GetComponent<Image>().sprite = Resources.Load<Sprite>("cell");
            }
        }
    }

    public void MakeAllCellsVisible()
    {
        foreach (Transform cell in this.transform) if (cell.name.Contains("image")) cell.GetComponent<Image>().color = Color.white;
    }
}
