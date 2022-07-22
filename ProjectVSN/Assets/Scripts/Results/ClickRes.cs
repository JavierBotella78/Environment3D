using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRes : MonoBehaviour
{
    private GameObject openPoint;

    private Transform prevParent;
    private Transform player;

    private Quaternion prevRot;
    private Vector3 initPos;

    private ViewController viewController;
    private VSNAsset actualAsset;

    [SerializeField]
    private bool openRes = false;

    void Start()
    {
        openPoint = GameObject.FindGameObjectWithTag("ViewPoint");
        player = GameObject.FindGameObjectWithTag("Player").transform;

        initPos = transform.position;

        viewController = GameObject.Find("ViewManager").GetComponent<ViewController>();
    }

    public void setAsset(VSNAsset datos)
    {
        actualAsset = datos;
    }

    public VSNAsset getAsset()
    {
        return actualAsset;
    }

    private void OnMouseUpAsButton()
    {
        if (!openRes)
        {
            viewController.OpenResult(openPoint, gameObject);
        }
        else
        {
            viewController.CloseResult();
        }
        
    }

}
