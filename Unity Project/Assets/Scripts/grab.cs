using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class grab : MonoBehaviour
{
    GameObject toCarry;
    bool carried;
    GameObject items;


    void Start()
    {
        items = GameObject.Find("Canvas").transform.GetChild(1).gameObject;


        carried = false;
        toCarry = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!carried) // ���� � ����� ����� ����
            {

                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit rH, 1.7f)) // ��������� ��������� ���� ���������
                {
                    if (rH.collider.gameObject.tag == "Interactable") // ���� ���������� ������� �� ����� �������� � ��������
                    {
                        carried = true;
                        toCarry = rH.collider.gameObject;
                        if (toCarry.GetComponent<Rigidbody>() != null) toCarry.GetComponent<Rigidbody>().isKinematic = true;
                        if (toCarry.GetComponent<MeshCollider>() != null) toCarry.GetComponent<MeshCollider>().enabled = false;
                    }

                    else if(rH.collider.gameObject.tag == "Collectable") // ���� ���������� ������� ����� �������� � ��������
                    {
                        GameObject item = new GameObject("tbr");
                        item.transform.parent = items.transform;
                        

                        if (rH.collider.name.Contains("Bread")) { item = CreateItemSprite(item, 1, 1, "Sprites/bread", "bread"); };


                        GameObject.Destroy(rH.collider.gameObject);
                        items.GetComponent<ItemController>().Add(item);
                    }
                };
            }


            else // ���� �������� ��� ���� ����� � ����
            {
                carried = false;
                if (toCarry.GetComponent<Rigidbody>() != null) toCarry.GetComponent<Rigidbody>().isKinematic = false;
                if (toCarry.GetComponent<MeshCollider>() != null) toCarry.GetComponent<MeshCollider>().enabled = true;
            }
        }

        if (Input.GetMouseButtonDown(0) && carried) // ������ ������� ����������� ��� (����� �����) | ���� ���� � ���� ���� �
        {
            carried = false;
            if (toCarry.GetComponent<Rigidbody>() != null)
            {
                toCarry.GetComponent<Rigidbody>().isKinematic = false;
                toCarry.GetComponent<Rigidbody>().AddForce(this.transform.forward * 10, ForceMode.Impulse);
            }
                if (toCarry.GetComponent<MeshCollider>() != null) toCarry.GetComponent<MeshCollider>().enabled = true;
        }


        if (carried) // ��������� �������� �������� ����� � �������
        {
            toCarry.transform.position = this.transform.position + this.transform.forward;
            toCarry.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }

        else { }

        Debug.DrawRay(transform.position, transform.forward * 1.7f, Color.cyan); // ����
    }


    // ��������� �������� ��'���� - ������� �������� � �������
    GameObject CreateItemSprite(GameObject @object /* ������������� ��'��� */, int x /* ����� �� �, �� ����� � ���. */, int y /* ����� �� �, �� ����� � ���. */, string TextureResourcePath, string nam) 
    {
        @object.AddComponent<MonoItemComp>().setComponent(x, y);
        RectTransform rect = @object.AddComponent<RectTransform>();
        rect.position = new Vector2(0, 0);
        rect.transform.SetParent(items.transform);

        float xsize = 1 + @object.GetComponent<MonoItemComp>().getX();
        float ysize = 1 + @object.GetComponent<MonoItemComp>().getY();

        rect.sizeDelta = new Vector2(78 * xsize, 78* ysize);

        Texture2D tx = Resources.Load<Texture2D>(TextureResourcePath); // ��������� �������� "�������", �� ������ ������� ��������
        Image image = @object.AddComponent<Image>();
        image.sprite = Sprite.Create(tx, new Rect(0, 0, tx.width, tx.height), new Vector2(0.5f, 0.5f));

        @object.name = nam; // ��'� ��'����

        return @object;
    }
}
