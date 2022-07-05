using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour
{
    private ViewController viewController;

    public GameObject toActive;
    public GameObject toDesactive;
    public Material hoverMaterial;

    private Material defaultMaterial;

    private void Start()
    {
        defaultMaterial = transform.GetComponent<Renderer>().material;
        viewController = GameObject.Find("ViewManager").GetComponent<ViewController>();
    }

    private void OnMouseUpAsButton()
    {
        viewController.ChangeView(toActive, toDesactive);
        gameObject.GetComponent<Renderer>().material = defaultMaterial;
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
