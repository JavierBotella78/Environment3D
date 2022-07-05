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


    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 newPos = move * Time.deltaTime * playerSpeed;

        controller.Move(newPos);
        //maximos y minimos movimiento
        if (transform.position.z > maxZ)
            transform.position = new Vector3(transform.position.x,transform.position.y, maxZ);
        if (transform.position.z < minZ)
            transform.position = new Vector3(transform.position.x, transform.position.y, minZ);

        if (transform.position.x >maxX)
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        if (transform.position.x < minX)
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);

    }
}
