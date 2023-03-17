using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ContextInvisOnExitMouse : MonoBehaviour, IPointerExitHandler
{
    GameObject button;

    public void OnPointerExit(PointerEventData eventData)
    {
        button = this.transform.GetChild(0).gameObject;

        if (button.GetComponent<Image>().enabled == true) { foreach (Transform button in this.transform) button.GetComponent<Image>().enabled = false; this.GetComponent<Image>().enabled = false; }
    }
}
