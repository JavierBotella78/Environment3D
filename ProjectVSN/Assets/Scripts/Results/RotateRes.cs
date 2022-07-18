using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRes : MonoBehaviour
{
    [SerializeField]
    private int rotateSpeed = 5;

    private Vector3 prevMousePos;
    private Material defaultMaterial;
    public Material clickMaterial;

    [SerializeField]
    private GameObject obj;


    // Start is called before the first frame update
    void Start()
    {
        prevMousePos = Vector3.zero;
        defaultMaterial = obj.transform.GetComponent<Renderer>().material;
    }
    
    private void OnMouseDown()
    {
        prevMousePos = Input.mousePosition;
        obj.transform.GetComponent<Renderer>().material = clickMaterial;
    }

    private void OnMouseUp()
    {
        prevMousePos = Vector3.zero;
        obj.GetComponent<Renderer>().material = defaultMaterial;
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


    private void DoTurn(float dir)
    {
        obj.transform.Rotate(0, (rotateSpeed * dir * Time.deltaTime), 0);
    }
}
