using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRes : MonoBehaviour
{
    public GameObject openPoint;
    private Vector3 initPos;
    public Material hoverMaterial;
    private Material defaultMaterial;
    private bool open = false;

    void Start()
    {
        defaultMaterial = transform.GetComponent<Renderer>().material;
        initPos = transform.position;
    }

    private void OnMouseUpAsButton()
    {
        open = !open;
        if (open)
        {
            initPos = transform.position;
            transform.position = openPoint.transform.position;
        }
        else
        {
            transform.position = initPos;
        }
        
    }

    private void OnMouseEnter()
    {
        transform.GetComponent<Renderer>().material = hoverMaterial;
    }

    private void OnMouseExit()
    {
        transform.GetComponent<Renderer>().material = defaultMaterial;
    }
}
