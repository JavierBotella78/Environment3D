using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkController : MonoBehaviour
{

    [SerializeField]
    private bool isEnabled = false;

    public NetworkResponse StartSearch(string url)
    {
        NetworkResponse result = new();

        if (isEnabled)
        {
            StartCoroutine(GetCoroutine(result, url));
        }

        return result;
    }

    // "https://catfact.ninja/fact"
    IEnumerator GetCoroutine(NetworkResponse result, string url)
    {
        // Se crea un objeto capaz de realizar llamadas GET a la url indicada
        UnityWebRequest www = UnityWebRequest.Get(url); // Llamada de prueba que da una curiosidad sobre gatos aleatoria

        yield return www.SendWebRequest();


        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            result.resultCode = www.result;
        }
        else
        {
            // Mostrar el resultado como texto (json)
            result.respText = www.downloadHandler.text;
            result.resultCode = www.result;

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
