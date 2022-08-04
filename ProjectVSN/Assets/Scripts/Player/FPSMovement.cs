using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMovement : MonoBehaviour
{
    private CharacterController cc;
    public float Gravedad = -9.81f;
    private Vector3 velocity;
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        cc = transform.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        cc.Move(move * speed * Time.deltaTime);

        //gravedad
        velocity.y += Gravedad * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}
