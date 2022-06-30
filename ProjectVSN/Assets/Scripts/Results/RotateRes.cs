using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRes : MonoBehaviour
{
    [SerializeField]
    private int rotateSpeed = 5;

    private Vector3 prevMousePos;
    private Material defaultMaterial;
    public Material hoverMaterial;
    public Material clickMaterial;
    private bool haveTurned = false;


    // Start is called before the first frame update
    void Start()
    {
        prevMousePos = Vector3.zero;
        defaultMaterial = transform.GetComponent<Renderer>().material;
    }
    
    private void OnMouseDown()
    {
        prevMousePos = Input.mousePosition;
        transform.GetComponent<Renderer>().material = clickMaterial;
    }

    private void OnMouseUp()
    {
        prevMousePos = Vector3.zero;
        haveTurned = false;
        gameObject.GetComponent<Renderer>().material = defaultMaterial;
    }
    private void OnMouseDrag()
    {
        Vector3 newMousePos = Input.mousePosition;
        if (!newMousePos.Equals(prevMousePos))
        {
            float tempDist = prevMousePos.x - newMousePos.x;
            if (tempDist < 0) //muevo hacia la izquierda
            {
                DoTurn(tempDist);
            }
            else //muevo hacia la derecha
            {
                DoTurn(tempDist);
            }
            prevMousePos = newMousePos;
        }
    }

    private void OnMouseOver()
    {
        if(!Input.GetMouseButton(0))
            transform.GetComponent<Renderer>().material = hoverMaterial;

    }

    private void OnMouseExit()
    {
        if (!Input.GetMouseButton(0))
            transform.GetComponent<Renderer>().material = defaultMaterial;
    }

    private void DoTurn(float dir)
    {
        //if (haveTurned == false)
        //{
            transform.Rotate(0, (rotateSpeed * dir * Time.deltaTime), 0);
            haveTurned = true;
        //}
    }
}
