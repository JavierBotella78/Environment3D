using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEvent : MonoBehaviour
{

    private Material defaultMaterial;
    public Material clickMaterial;

    private void Start()
    {
        defaultMaterial = transform.GetComponent<Renderer>().material;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            transform.GetComponent<Renderer>().material = clickMaterial;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            transform.GetComponent<Renderer>().material = defaultMaterial; //drop
        }



    }
}
