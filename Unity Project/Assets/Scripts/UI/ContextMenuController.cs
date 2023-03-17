using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenuController : MonoBehaviour
{
    GameObject use;
    GameObject equip;
    GameObject drop;


    private void Start()
    {
        use = this.transform.GetChild(0).GetChild(0).gameObject;
        equip = this.transform.GetChild(0).GetChild(1).gameObject;
        drop = this.transform.GetChild(0).GetChild(2).gameObject;
    }

    public void ButtonCheck(GameObject a)
    {
        itemType type = GameObject.Find("Items").GetComponent<ItemDescriptor>().getTypeByName(a.name);

        switch (type)
        {
            case itemType.Weapon: SetButtonInteractablity(use, false); SetButtonInteractablity(equip, true); SetButtonInteractablity(drop, true); break;
            case itemType.Food: SetButtonInteractablity(use, true); SetButtonInteractablity(equip, false); SetButtonInteractablity(drop, true); break;
            case itemType.Quickuse: SetButtonInteractablity(use, true); SetButtonInteractablity(equip, true); SetButtonInteractablity(drop, true); break;
        }
    }

    public void SetButtonInteractablity(GameObject a, bool active)
    {
        if (!active)
        {
            a.GetComponent<Button>().interactable = false;
            a.GetComponent<Image>().color = new Color(1, 1, 1, 0.3f);
        }

        else
        {
            a.GetComponent<Button>().interactable = true;
            a.GetComponent<Image>().color = Color.white;
        }
    }
}
