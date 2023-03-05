using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class Highlight_Collectable : MonoBehaviour
{
    GameObject highlighter;
    IEnumerator changer;

    private void OnMouseEnter()
    {
        //Debug.Log("Mouse Enter");

        if(highlighter == null){ highlighter = GameObject.Instantiate(this.gameObject, this.transform, true);

            highlighter.transform.parent = null;
        GameObject.Destroy(highlighter.GetComponent<Rigidbody>());
        GameObject.Destroy(highlighter.GetComponent<MeshCollider>());
        highlighter.transform.localScale = this.transform.localScale * 1.02f;


        highlighter.GetComponent<Renderer>().material = Resources.Load<Material>("Materials/Transparent");

        StartCoroutine(changer = alphachanger(highlighter.GetComponent<Renderer>()));
        //highlighter.GetComponent<MeshFilter>().mesh = null;
    }}

    private void OnMouseExit()
    {
        StopCoroutine(changer);
        Destroy(highlighter);
    }

    public void OnDestroy()
    {
        Destroy(highlighter);
    }

    IEnumerator alphachanger(Renderer r)
    {
        bool backwards = false;

        Debug.Log("Coroutine started");

        while (true)
        {
            
            if(!backwards)
                    for (float i = 0.1f; i <= 0.14f; i += 0.02f)
                    {
                        Color color = r.material.color;
                        color.a = i;

                        r.material.color = color;
                        yield return new WaitForSecondsRealtime(0.15f);
                    }
            else
                    for(float i = 0.14f; i>=0.1f; i -= 0.02f)
                    {
                        Color color = r.material.color;
                        color.a = i;

                        r.material.color = color;
                        yield return new WaitForSecondsRealtime(0.15f);
                    }

            backwards = !backwards;
        }   
    }
}
