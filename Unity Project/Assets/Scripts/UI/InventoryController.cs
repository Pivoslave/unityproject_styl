using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] bool wided = false;


    // Start is called before the first frame update
    void Start()
    {

        if (!wided)
        {
            GameObject cell = new GameObject("testcell");

            RectTransform pos = cell.AddComponent<RectTransform>();
            pos.transform.SetParent(this.transform);
            pos.anchoredPosition = new Vector2(-918f, 426.65f);
            pos.sizeDelta = new Vector2(78.01f, 78.01f);

            Image image = cell.AddComponent<Image>();

            cell.transform.SetParent(this.transform);



            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 6; i++)
                {
                    GameObject cell2 = GameObject.Instantiate(cell, this.transform);
                    cell2.gameObject.name = "image_" + j.ToString() + "_" + i.ToString();
                    cell2.GetComponent<RectTransform>().anchoredPosition =cell.GetComponent<RectTransform>().anchoredPosition + new Vector2(cell.GetComponent<RectTransform>().sizeDelta.x * i, cell.GetComponent<RectTransform>().sizeDelta.y * -j);
                    InventoryCell incell = cell2.AddComponent<InventoryCell>();
                    incell.setxy(j, i);
                }

            }

            GameObject.Destroy(cell);
        }

        else if (wided) { };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
