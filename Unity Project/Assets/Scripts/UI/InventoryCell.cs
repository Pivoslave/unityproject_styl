using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] public int posx;
    [SerializeField] public int posy;
    [SerializeField] bool isOccupied;

    bool lastOccupied;

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
    public void changeStatus(bool occupied) { lastOccupied = isOccupied; if (occupied) isOccupied = true; else isOccupied = false; }

    private void Update()
    {
        if(isOccupied != lastOccupied)
        {
            if (isOccupied) this.GetComponent<Image>().color = new Color(1, 1, 1, 0);
            else this.GetComponent<Image>().color = Color.white;
        }
    }
}
