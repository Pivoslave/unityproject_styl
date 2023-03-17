using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    ItemDescriptor descriptor;
    ItemController controller;
    Camera mainCam;
    LogicArrayInv logic;


    public GameObject invitem;

    private void Start()
    {
        descriptor = GameObject.Find("Items").GetComponent<ItemDescriptor>();
        mainCam = GameObject.Find("Capsule").transform.GetChild(0).GetComponent<Camera>();
        logic = GameObject.Find("Inventory_Cells").GetComponent<LogicArrayInv>();
        controller = GameObject.Find("Items").GetComponent<ItemController>();
    }

    public void DropItem()
    {
        GameObject dropped = descriptor.getDroppable(invitem);

        GameObject dr = GameObject.Instantiate(dropped, (mainCam.transform.position + mainCam.transform.forward * 1.1f), Quaternion.LookRotation(mainCam.transform.forward * 1.1f, Vector3.up), null);
        dr.name = descriptor.getNameByInvItem(invitem);

        item_location coords = controller.FindByGameObject(invitem);
        bool rotated = invitem.GetComponent<RectTransform>().pivot == Vector2.zero ? true : false;
        logic.ChangeCellStatus(false, coords.GetFirstCell().GetX(), coords.GetFirstCell().GetY(), coords.GetLastCell().GetX(), coords.GetLastCell().GetY(), rotated);
        controller.Remove(coords);
        GameObject.Destroy(invitem);
    }
}
