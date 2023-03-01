using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class grab : MonoBehaviour
{
    GameObject toCarry;
    bool carried;
    GameObject items;
    GameObject inventory;

    //ItemDescriptor descriptor;
    //RectTransform CellToPut;

    

    void Start()
    {
        items = GameObject.Find("Canvas").transform.GetChild(1).gameObject;
        inventory = GameObject.Find("Canvas").transform.GetChild(0).gameObject;


        //descriptor = items.GetComponent<ItemDescriptor>();

        carried = false;
        toCarry = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!carried) // Якщо у руках нічого немає
            {

                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit rH, 2.4f)) // Дистанція витягнутої руки персонажа
                {
                    if (rH.collider.gameObject.tag == "Interactable") // Якщо зачеплений предмет не можна покласти у інвентар
                    {
                        carried = true;
                        toCarry = rH.collider.gameObject;
                        if (toCarry.GetComponent<Rigidbody>() != null) toCarry.GetComponent<Rigidbody>().isKinematic = true;
                        if (toCarry.GetComponent<MeshCollider>() != null) toCarry.GetComponent<MeshCollider>().enabled = false;
                    }

                    else if(rH.collider.gameObject.tag == "Collectable") // Якщо зачеплений предмет можна покласти у інвентар
                    {
                        rect_rot cell_rotation = new rect_rot(null, false); // перша комірка що підходить + чи повернутий має бути предмет
                        inventoryItem item_descriptor = new inventoryItem("", "", 0, 0);
                        GameObject item = new GameObject("");

                        item_descriptor = items.GetComponent<ItemDescriptor>().StructByGameObject(rH.collider.gameObject);
                        
                        cell_rotation = inventory.GetComponent<LogicArrayInv>().FindFirst(item_descriptor.x, item_descriptor.y);


                        // перепиСАТЬ
                        if (cell_rotation.rect != null && item_descriptor.x != -1)
                        {
                            GameObject.Destroy(rH.collider.gameObject);

                            Vector3 cell_left_upper = cell_rotation.rect.position - new Vector3(cell_rotation.rect.position.x / 2, -cell_rotation.rect.position.y / 2);
                            Vector3 center_offset = Vector3.zero;

                            CreateItemSprite(item, cell_rotation, item_descriptor);

                            //CreateItemSprite(item, item_descriptor.x, item_descriptor.y, item_descriptor.imagepath, item_descriptor.itemname);

                            //if (!cell_rotation.is_rotated)
                              //  center_offset = new Vector3(item.GetComponent<RectTransform>().sizeDelta.x/2, -item.GetComponent<RectTransform>().sizeDelta.y/2);

                            //else if (cell_rotation.is_rotated)
                              //  center_offset = new Vector3(item.GetComponent<RectTransform>().sizeDelta.y/2, -item.GetComponent<RectTransform>().sizeDelta.x/2);

                            //item.GetComponent<RectTransform>().position = cell_left_upper - center_offset;
                            //if (cell_rotation.is_rotated) item.GetComponent<RectTransform>().rotation = Quaternion.Euler(0, 0, 90);

                            inventory.GetComponent<LogicArrayInv>().ChangeCellStatus(true, cell_rotation.rect.position.x, cell_rotation.rect.position.y, item_descriptor.x, item_descriptor.y);
                        }

                        else if (cell_rotation.rect == null) { };


                     //   cell_rotation = inventory.GetComponent<LogicArrayInv>().FindFirst(descriptor.bread.x, descriptor.bread.y);
                     //   item = CreateItemSprite(item, descriptor.bread.x, descriptor.bread.y, descriptor.bread.imagepath, descriptor.bread.itemname);

                        items.GetComponent<ItemController>().Add(item);
                    }
                };
            }


            else // Якщо персонаж вже щось тримає у руці
            {
                carried = false;
                if (toCarry.GetComponent<Rigidbody>() != null) toCarry.GetComponent<Rigidbody>().isKinematic = false;
                if (toCarry.GetComponent<MeshCollider>() != null) toCarry.GetComponent<MeshCollider>().enabled = true;
            }
        }

        if (Input.GetMouseButtonDown(0) && carried) // Кинути предмет натисканням ЛКМ (фізґан стайл) | лише якщо у руці щось є
        {
            carried = false;
            if (toCarry.GetComponent<Rigidbody>() != null)
            {
                toCarry.GetComponent<Rigidbody>().isKinematic = false;
                toCarry.GetComponent<Rigidbody>().AddForce(this.transform.forward * 10, ForceMode.Impulse);
            }
                if (toCarry.GetComponent<MeshCollider>() != null) toCarry.GetComponent<MeshCollider>().enabled = true;
        }


        if (carried) // Контролер тримання предмету поруч з гравцем
        {
            toCarry.transform.position = this.transform.position + this.transform.forward;
            toCarry.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        else { }

        Debug.DrawRay(transform.position, transform.forward * 1.7f, Color.cyan); // Дебаґ
    }


    /// <summary>
    /// Створення ігрового об'єкта - спрайта предмета у рюкзаку
    /// </summary>
    /// <param name="object">Першопочатковий об'єкт</param> 
    /// <param name="x">зміщення за х</param>
    /// <param name="y">зміщення за у</param>
    /// <param name="TextureResourcePath">шлях до спрайту у папці "Resources"</param>
    /// <param name="nam">ім'я створеного об'єкта</param>
    /// <returns></returns>
    void CreateItemSprite(GameObject @object, int x, int y, string TextureResourcePath, string nam) 
    {
        @object.transform.parent = items.transform;
        @object.AddComponent<MonoItemComp>().setComponent(x, y);
        RectTransform rect = @object.AddComponent<RectTransform>();
        rect.position = new Vector2(0, 0);
        rect.transform.SetParent(items.transform);

        float xsize = 1 + @object.GetComponent<MonoItemComp>().getX();
        float ysize = 1 + @object.GetComponent<MonoItemComp>().getY();

        rect.sizeDelta = new Vector2(78 * xsize, 78* ysize);

        Texture2D tx = Resources.Load<Texture2D>(TextureResourcePath); // директорія всередині "Ресурсів", що містить потрібну текстуру
        Image image = @object.AddComponent<Image>();
        image.sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), new Vector2(0.5f, 0.5f));

        @object.name = nam; // Ім'я об'єкта
    }

    /// <summary>
    /// Створення ігрового об'єкта - спрайта предмета у рюкзаку
    /// </summary>
    /// <param name="object">Першопочатковий об'єкт</param> 
    /// <param name="r_r"></param>
    /// <param name="descriptor"></param>
    void CreateItemSprite(GameObject @object, rect_rot r_r, inventoryItem descriptor)
    {
        @object.transform.parent = inventory.transform;

        @object.name = descriptor.itemname;

        @object.AddComponent<MonoItemComp>().setComponent(descriptor.x, descriptor.y);
        RectTransform rect = @object.AddComponent<RectTransform>();

        if (!r_r.is_rotated) rect.pivot = new Vector2(0, 1);
        else rect.pivot = Vector2.zero;

        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;

        RectTransform firstcell = r_r.rect;
        RectTransform lastcell = null;

        @object.transform.localScale = Vector3.one;

        foreach(Transform cell in inventory.transform)
        {
            if(cell.GetComponent<InventoryCell>() != null && 
                cell.GetComponent<InventoryCell>().posx == firstcell.gameObject.GetComponent<InventoryCell>().posx + descriptor.x &&
                cell.GetComponent<InventoryCell>().posy == firstcell.gameObject.GetComponent<InventoryCell>().posy + descriptor.y)
            {
                lastcell = cell.GetComponent<RectTransform>();
                break;
            }
        }

        float size = inventory.GetComponent<InventoryController>().GetCellSize();
        InventoryController inv_cont = inventory.GetComponent<InventoryController>();

        Vector2 max = new Vector2(-(inv_cont.GetWidth() - (firstcell.offsetMin.x + ((1 + descriptor.x) * size))), 0f);
        max.y = -(inv_cont.GetHeight() - firstcell.offsetMin.y - ((1 + descriptor.y) * size));

        rect.offsetMin = firstcell.offsetMin;
        rect.offsetMax = new Vector2(-(inv_cont.GetWidth() - (firstcell.offsetMin.x + ((1 + descriptor.x) * size))), -(inv_cont.GetHeight() - firstcell.offsetMin.y - ((1 + descriptor.y) * size)));

        rect.position = firstcell.position;

        Texture2D tx = Resources.Load<Texture2D>(descriptor.imagepath); // директорія всередині "Ресурсів", що містить потрібну текстуру
        Image image = @object.AddComponent<Image>();
        image.sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), new Vector2(0.5f, 0.5f));
    }
}
