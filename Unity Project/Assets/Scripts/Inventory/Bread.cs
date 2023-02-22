using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// DEPRECATED FOR NOW
public class Bread 
{
    public struct xy
    {
        int x, y;

        public xy(int _x, int _y)
        {
            x = _x; y = _y;
        }
    }

    public xy[] crd = new xy[2] {new xy(1, 1), new xy(1, 1) };
}
