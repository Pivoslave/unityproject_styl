using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonoItemComp : MonoBehaviour
{
    struct xy
    {
        int x, y;

        public xy(int _x, int _y)
        {
            x= _x;
            y= _y;
        }
    };

    xy[] offsets = new xy[2];
    int x, y;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // виставляє зміщення розміру предмета відносно (0; 0), дає змогу отримати ці значення у наступних функціях
    //нема різниці, чи вводити ширину-висоту чи висоту-ширину
    public void setComponent(int _x, int _y) 
    {
        offsets[0] = new xy(_x, _y);
        offsets[1] = new xy(_y, _x);
        x = _x;
        y = _y;
    }

    // отримати х
    public float getX() { return x; }

    //отримати у
    public float getY() { return y; }
}
