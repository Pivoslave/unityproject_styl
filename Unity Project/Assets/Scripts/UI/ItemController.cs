using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    //struct item_index_pair
    //{
    //    public GameObject item;
    //    public int index;

    //    public item_index_pair(GameObject go, int ind)
    //    {
    //        item = go;
    //        index = ind;
    //    }

    //    public GameObject GameObject_by_index (int dex) { if(index == dex) return item; else return null; }
    //    public int Index_by_GameObject(GameObject go) { if(item == go) return index; else return -1; }
    //}

    [SerializeField] List<GameObject> items = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // додати елемент до списку
    public void Add(GameObject a)
    {
        items.Add(a);
    }

    // виключити елемент зі списку
    public void Remove(GameObject a)
    {
        items.Remove(a);
    }

    //public int GetLastIndex() {
    //    if (items.Count != 0) return items.LastOrDefault<item_index_pair>().index;
    //    else return -1;
    //}

    public int getItemIndex(GameObject a)
    {
        if (a != null)
            return items.IndexOf(a);
        else return -1;
    }
}
