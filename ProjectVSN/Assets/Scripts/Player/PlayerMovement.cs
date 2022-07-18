using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float maxX=0.5f;
    [SerializeField]
    private float minX=-0.5f;
    [SerializeField]
    private float maxZ=1f;
    [SerializeField]
    private float minZ=0f;

    private Vector3 lastPos;


    private void Start()
    {
        lastPos = transform.position;
        controller = gameObject.GetComponent<CharacterController>();
        ViewController.OnViewChanged += PositionUpdate;
    }

    private void OnDestroy()
    {
        ViewController.OnViewChanged -= PositionUpdate;
    }

    void Update()
    {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 newPos = move * Time.deltaTime * playerSpeed;

        // Si no se mueve el jugador, no se actualiza la posicion
        if (newPos != Vector3.zero)
        {
            controller.Move(newPos);
            //maximos y minimos movimiento
            if (transform.position.z > lastPos.z + maxZ)
                transform.position = new Vector3(transform.position.x, transform.position.y, lastPos.z + maxZ);
            if (transform.position.z < lastPos.z + minZ)
                transform.position = new Vector3(transform.position.x, transform.position.y, lastPos.z + minZ);

            if (transform.position.x > maxX)
                transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
            if (transform.position.x < minX)
                transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }
    }

    public void PositionUpdate()
    {
        lastPos = transform.position;
    }
}
