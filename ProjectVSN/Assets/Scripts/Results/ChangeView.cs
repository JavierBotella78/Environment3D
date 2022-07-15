using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour
{
    private ViewController viewController;

    [SerializeField]
    private bool returnSearch = false;

    private void Start()
    {
        viewController = GameObject.Find("ViewManager").GetComponent<ViewController>();
    }

    private void OnMouseUpAsButton()
    {
        if (returnSearch)
            viewController.ReturnToSearch();
        else
            viewController.ChangeView();

    }

}
