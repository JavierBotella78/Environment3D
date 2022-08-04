using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCam : MonoBehaviour
{
    public float sensibilidad = 50f;
    [SerializeField]
    private Transform playerBody;
    private float xRotation;
    public Texture2D pointerText = null;

    // Start is called before the first frame update
    void Start()
    {
        //Cambio cursor
        Cursor.SetCursor(pointerText, Vector2.zero, CursorMode.Auto);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        //rotamos el objeto padre
        playerBody.Rotate(Vector2.up * mouseX);

        //giramos la camara arriba y abajo
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
