using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using UnityEngine;

public class LogicArrayInv : MonoBehaviour
{

    // інтегрувати як вивід ф-ції FindFirst для випадків, коли предмет повернуто на 90 градусів
    struct rect_rot
    {
        RectTransform rect;
        bool is_rotated;
    }

    bool[,] occup;

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

    // Доповнити ф-цію для повернутого предмета, змінити тип вихідних даних на структуру описану на початку скрипта
    public RectTransform FindFirst(int x, int y)
    {
        for (int i = 0; i <= xsize; i++){ // i <=> x
            for (int j = 0; j <= ysize; j++){ // j <=> y
                
                if(i + x <= xsize && j + y <= ysize){ // пошук не повинен виходити за межі комірок інвентаря
                    
                    // тут починається безпосередня перевірка комірок
                    bool cellAvailable = true;

                    string output = "";

                    for(int i1 = 0; i1<=x; i1++){
                        for(int j1 = 0; j1<=y; j1++){

                            output = output + "(" + (i + i1).ToString() + (j + j1).ToString() + ")";

                            if (occup[i + i1, j + j1] == true) cellAvailable = false; 
                        }
                    }

                    Debug.Log(output);


                    if(cellAvailable) {
                        foreach (Transform cell in this.transform)
                            if (cell.GetComponent<InventoryCell>() != null){
                                InventoryCell incell = cell.GetComponent<InventoryCell>();
                                if(incell.posx == i && incell.posy == j)
                                {
                                    Debug.Log(cell.name);
                                    return cell.gameObject.GetComponent<RectTransform>();
                                }
                            }
                    }
                }
            }
        }

        return null;
    }

    public void GetInfo()
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


}
