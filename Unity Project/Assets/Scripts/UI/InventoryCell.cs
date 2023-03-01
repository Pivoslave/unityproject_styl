using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] public int posx;
    [SerializeField] public int posy;
    [SerializeField] bool isOccupied;

    public void setxy(int x, int y) { // викликається при ініціалізації комірок інвентаря програмно
        this.posx = x;
        this.posy = y;
        isOccupied = false;
    }

    public bool getStatus() { return isOccupied; }

    /// <summary>
    /// змінює статус комірки
    /// </summary>
    /// <param name="occupied"> <b><i>true</i></b> клітина займається, <b><i>false</i></b> - звільняється - </param>
    public void changeStatus(bool occupied) { if (occupied) isOccupied = true; else isOccupied = false; } 
}
