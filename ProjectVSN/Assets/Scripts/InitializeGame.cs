using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    public GameObject toActive;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] vistas=GameObject.FindGameObjectsWithTag("View");
        foreach (GameObject actual in vistas)
        {
            actual.SetActive(false);
        }
        toActive.SetActive(true);
    }
}
