using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public float rotateSpeed = 500.0f;

    private void Start()
    {

    }

    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;

        float h = Time.deltaTime * rotateSpeed * Input.GetAxisRaw("Mouse X");
        float v = Time.deltaTime * rotateSpeed * Input.GetAxisRaw("Mouse Y");

        Vector3 rotPlayer = transform.rotation.eulerAngles;

        rotPlayer.x -= v;
        rotPlayer.z = 0;
        rotPlayer.y += h;

        transform.rotation = Quaternion.Euler(rotPlayer);

        float horizMov = Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime;
        float vertiMov = Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime;

        if (horizMov != 0f || vertiMov != 0f)
        {
            float rotY = (transform.rotation.eulerAngles.y * Mathf.PI/180.0f);

            float hX = -Mathf.Sin(rotY - Mathf.PI / 2);
            float hZ = -Mathf.Cos(rotY - Mathf.PI / 2);

            float vX = Mathf.Sin(rotY);
            float vZ = Mathf.Cos(rotY);

            float xTotal = vertiMov * vX + horizMov * hX;
            float zTotal = vertiMov * vZ + horizMov * hZ;

            transform.position += new Vector3(xTotal, 0f, zTotal);
        }
    }
}
