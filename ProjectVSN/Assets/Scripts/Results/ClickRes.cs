using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRes : MonoBehaviour
{
    private GameObject openPoint;
    private Renderer planeBack;

    private Transform prevParent;
    private Transform player;

    private Quaternion prevRot;
    private Vector3 initPos;

    public Material hoverMaterial;
    private Material defaultMaterial;
    private bool open = false;

    void Start()
    {
        openPoint = GameObject.FindGameObjectWithTag("ViewPoint");
        player = GameObject.FindGameObjectWithTag("Player").transform;
        planeBack = gameObject.transform.Find("PlaneBack").gameObject.GetComponent<Renderer>();

        defaultMaterial = planeBack.material;
        initPos = transform.position;
    }

    private void OnMouseUpAsButton()
    {
        open = !open;
        if (open)
        {
            prevRot = transform.rotation;
            transform.rotation = Quaternion.Euler(new Vector3(90,0,180));
            initPos = transform.position;
            transform.position = openPoint.transform.position;

            prevParent = gameObject.transform.parent;
            gameObject.transform.SetParent(player);
        }
        else
        {
            transform.rotation = prevRot;
            transform.position = initPos;

            gameObject.transform.SetParent(prevParent);
        }
        
    }

    private void OnMouseEnter()
    {
        planeBack.material = hoverMaterial;
    }

    private void OnMouseExit()
    {
        planeBack.material = defaultMaterial;
    }
}
