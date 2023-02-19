using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryCell : MonoBehaviour
{
    [SerializeField] public int posx;
    [SerializeField] public int posy;

    public void setxy(int x, int y) {
        this.posx = x;
        this.posy = y;
    }
}
