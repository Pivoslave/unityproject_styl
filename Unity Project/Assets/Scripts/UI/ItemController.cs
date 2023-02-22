using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    [SerializeField] List<GameObject> items = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add(GameObject a)
    {
        items.Add(a);
    }

    public void Remove(GameObject a)
    {
        items.Remove(a);
    }
}
