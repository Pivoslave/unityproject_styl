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

    // ��������� ������� ������ �������� ������� (0; 0), �� ����� �������� �� �������� � ��������� ��������
    //���� ������, �� ������� ������-������ �� ������-������
    public void setComponent(int _x, int _y) 
    {
        offsets[0] = new xy(_x, _y);
        offsets[1] = new xy(_y, _x);
        x = _x;
        y = _y;
    }

    // �������� �
    public float getX() { return x; }

    //�������� �
    public float getY() { return y; }
}
