using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    public GameObject toActive;

    [SerializeField]
    private GameObject[] toDesactive;

    

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] vistas=GameObject.FindGameObjectsWithTag("View");
        foreach (GameObject actual in vistas)
        {
            actual.SetActive(false);
        }
        toActive.SetActive(true);

        foreach (GameObject actual in toDesactive)
            actual.SetActive(false);

        Cursor.lockState = CursorLockMode.None;

    }
}
