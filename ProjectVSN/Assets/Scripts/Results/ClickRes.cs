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

    private bool open = false;

    void Start()
    {
        openPoint = GameObject.FindGameObjectWithTag("ViewPoint");
        player = GameObject.FindGameObjectWithTag("Player").transform;

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

}
