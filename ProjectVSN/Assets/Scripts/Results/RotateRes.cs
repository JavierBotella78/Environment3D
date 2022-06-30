using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRes : MonoBehaviour
{
    private Vector3 initMousePos;
    private Material defaultMaterial;
    public Material hoverMaterial;
    public Material clickMaterial;
    private bool haveTurned = false;


    // Start is called before the first frame update
    void Start()
    {
        initMousePos = Vector3.zero;
        defaultMaterial = transform.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseDown()
    {
        initMousePos = Input.mousePosition;
        transform.GetComponent<Renderer>().material = clickMaterial;
    }

    private void OnMouseUp()
    {
        initMousePos = Vector3.zero;
        haveTurned = false;
        gameObject.GetComponent<Renderer>().material = defaultMaterial;
    }
    private void OnMouseDrag()
    {
        Vector3 newMousePos = Input.mousePosition;
        if(!newMousePos.Equals(initMousePos))
            if (newMousePos.x < initMousePos.x) //muevo hacia la izquierda
            {
                DoTurn(-1);
            }
            else //muevo hacia la derecha
            {
                DoTurn(1);
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

    private void DoTurn(int dir)
    {
        if (haveTurned == false)
        {
            transform.Rotate(0, (45 * dir), 0);
            haveTurned = true;
        }
    }
}
