using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkController : MonoBehaviour
{

    [SerializeField]
    private bool isEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isEnabled)
            StartCoroutine(GetCoroutine());
    }

    IEnumerator GetCoroutine() 
    {
        // Se crea un objeto capaz de realizar llamadas GET a la url indicada
        UnityWebRequest www = UnityWebRequest.Get("https://catfact.ninja/fact"); // Llamada de prueba que da una curiosidad sobre gatos aleatoria

        yield return www.SendWebRequest();


        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Mostrar el resultado como texto (json)
            Debug.Log(www.downloadHandler.text);

            // O recibir los datos binarios
            byte[] results = www.downloadHandler.data;
        }

    }

    // Como no creo que se realicen ninguna llama POST, se comenta por si acaso
    /*
    IEnumerator PostCoroutine()
    {
        UnityWebRequest www = UnityWebRequest.Post(@"url\to\post", "Test");

        yield return www.SendWebRequest();


        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Post success
            Debug.Log("Success");
        }


    }
    */
}
