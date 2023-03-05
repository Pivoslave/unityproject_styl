using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Цей клас містить функції та змінні, що відповідають за промальовку комірок інвентаря.
/// </summary>

public class InventoryController : MonoBehaviour
{
    [SerializeField] bool wided = false;

    GameObject setting_table;
    GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        //wided = true;
        DrawCells();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DrawCells()
    {

        int hor, vert;

        vert = 6;
        hor = wided == true ? 16 : 6;

        setting_table = this.transform.Find("setting_table").gameObject;
        canvas = this.gameObject;

        float width = canvas.GetComponent<RectTransform>().sizeDelta.x;
        float height = canvas.GetComponent<RectTransform>().sizeDelta.y;

        float size = Mathf.Abs((width + setting_table.GetComponent<RectTransform>().offsetMax.x - 9) / 16);

        GameObject cell = new GameObject("testcell");

        RectTransform pos = cell.AddComponent<RectTransform>();
        pos.transform.SetParent(this.transform);

        pos.pivot = new Vector2(0, 1);
        pos.anchorMax = new Vector2(1, 1);
        pos.anchorMin = new Vector2(0, 0);

        pos.offsetMin = new Vector2(9, height - 10 - size);
        pos.offsetMax = new Vector2(-(width - (9 + size)), -10);

        cell.transform.localScale = Vector3.one;

        Image image = cell.AddComponent<Image>();
        Texture2D tx = Resources.Load<Texture2D>("cell");
        image.sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), new Vector2(0, 1));

        image.enabled = false;

        cell.transform.SetParent(this.transform);

        for (int j = 0; j < vert; j++)
        {
            for (int i = 0; i < hor; i++)
            {
                GameObject cell2 = GameObject.Instantiate(cell, this.transform);

                cell2.gameObject.name = "image_" + i.ToString() + "_" + j.ToString();

                cell2.GetComponent<RectTransform>().offsetMin = cell.GetComponent<RectTransform>().offsetMin + new Vector2(size * i, -size * j);
                cell2.GetComponent<RectTransform>().offsetMax = cell.GetComponent<RectTransform>().offsetMax + new Vector2(size * i, -size * j);

                InventoryCell incell = cell2.AddComponent<InventoryCell>();
                incell.setxy(i, j);
                cell2.AddComponent<Cell_to_Drag>();
                cell2.AddComponent<GraphicRaycaster>().enabled = false;
            }
        }

        GameObject.Destroy(cell);

        this.GetComponent<LogicArrayInv>().FormArray();
        
    }


    public float GetCellSize()
    {
        float width = canvas.GetComponent<RectTransform>().sizeDelta.x;
        return Mathf.Abs((width + setting_table.GetComponent<RectTransform>().offsetMax.x - 9) / 16);
    }

    public float GetWidth()
    {
        return canvas.GetComponent<RectTransform>().sizeDelta.x;
    }

    public float GetHeight()
    {
        return canvas.GetComponent<RectTransform>().sizeDelta.y;
    }
}
