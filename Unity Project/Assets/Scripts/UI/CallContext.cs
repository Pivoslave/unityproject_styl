using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CallContext : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool entered;
    public GameObject chosen;

    public void OnPointerEnter(PointerEventData eventData)
    {
        entered = true;
        chosen = this.gameObject;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        entered = false;
        chosen = null;

    }

    private void Update()
    {
        if(entered && Input.GetMouseButtonDown(1))
        {
            GameObject.Find("ContextMenu").transform.GetChild(0).GetComponent<Image>().enabled = false;
            foreach (Transform button in GameObject.Find("ContextMenu").transform.GetChild(0)) button.GetComponent<Image>().enabled = true;
            GameObject.Find("ContextMenu").GetComponent<ContextMenuController>().ButtonCheck(chosen);
            GameObject.Find("ContextMenu").transform.GetChild(0).transform.position = Input.mousePosition;
            
            SendInfoToButtons();
        }
    }

    private void SendInfoToButtons()
    {
        GameObject.Find("ContextMenu").transform.GetChild(0).GetChild(2).GetComponent<Drop>().invitem = chosen;
    }
}
