using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour
{
    public GameObject toActive;
    public GameObject toDesactive;
    public Material hoverMaterial;

    private Material defaultMaterial;

    [SerializeField]
    private bool searchActivated = false;

    public delegate void StartSearch();
    public static event StartSearch OnSearchStart;

    private void Start()
    {
        defaultMaterial = transform.GetComponent<Renderer>().material;
    }

    private void OnMouseUpAsButton()
    {
        toDesactive.SetActive(false);
        toActive.SetActive(true);
        gameObject.GetComponent<Renderer>().material = defaultMaterial;

        if (searchActivated && OnSearchStart != null)
        {
            OnSearchStart();
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
