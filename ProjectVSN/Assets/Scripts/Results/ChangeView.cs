using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour
{
    private ViewController viewController;

    [SerializeField]
    private Material hoverMaterial;
    private Material defaultMaterial;

    [SerializeField]
    private bool returnSearch = false;

    private void Start()
    {
        defaultMaterial = transform.GetComponent<Renderer>().material;
        viewController = GameObject.Find("ViewManager").GetComponent<ViewController>();
    }

    private void OnMouseUpAsButton()
    {
        if (returnSearch)
            viewController.ReturnToSearch();
        else
            viewController.ChangeView();

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
