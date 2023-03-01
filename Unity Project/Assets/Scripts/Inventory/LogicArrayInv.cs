using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using UnityEngine;

// ����������� �� ���� �-��� FindFirst ��� �������, ���� ������� ��������� �� 90 �������
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


    // ��������� �-��� ��� ����������� ��������, ������ ��� �������� ����� �� ��������� ������� �� ������� �������
    public RectTransform FindFirstOneDim(int x, int y)
    {
        for (int i = 0; i <= xsize; i++){ // i <=> x
            for (int j = 0; j <= ysize; j++){ // j <=> y
                
                if(i + x <= xsize && j + y <= ysize){ // ����� �� ������� �������� �� ��� ������ ���������
                    
                    // ��� ���������� ������������ �������� ������
                    bool cellAvailable = true; // �������� ������ �� ������������� - ���������

                    string output = ""; // ����

                    for(int i1 = 0; i1<=x; i1++){ // �������� �0 + (0; �)
                        for(int j1 = 0; j1<=y; j1++){ // �������� �0 + (0; �)

                            output = output + "(" + (i + i1).ToString() + (j + j1).ToString() + ")"; // ����

                            if (occup[i + i1, j + j1] == true) cellAvailable = false; // ���� ������ �������, ��������� ��������
                        }
                    }

                    Debug.Log(output); // ����


                    if(cellAvailable) { // ���� �� �������� ������ ������� 
                        foreach (Transform cell in this.transform) 
                            if (cell.GetComponent<InventoryCell>() != null){
                                InventoryCell incell = cell.GetComponent<InventoryCell>();
                                if(incell.posx == i && incell.posy == j) // ���������� ������ � ������������ � ���������� � ������� ������
                                {
                                    Debug.Log(cell.name);
                                    return cell.gameObject.GetComponent<RectTransform>(); // � ����������� �� Transform 
                                }
                            }
                    }
                }
            }
        }

        return null; // ������ �� ����������� �����;
    }

    public void GetInfo() // ���������� ������ ������  | ����
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
    /// ����� ������ �������� ������ �� true ��� false
    /// </summary>
    /// <param name="occupying">true - ����� ������, false - �������</param> 
    /// <param name="posx"> ���������� � ��� ������� ������ </param>
    /// <param name="posy"> ���������� � ��� ������� ������ </param>
    /// <param name="x"> ���� �������� �� � ������� ��� ������� ������ </param>
    /// <param name="y"> ���� �������� �� � ������� ��� ������� ������ </param>
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
    }
}
