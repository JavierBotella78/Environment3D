using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkController : MonoBehaviour
{

    [SerializeField]
    private bool isEnabled = false;

    public string[] results = { "" };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartSearch(int numResults)
    {
        if (isEnabled)
        {
            results = new string[numResults];
            StartCoroutine(LoopGet(results, numResults));
        }
    }

    IEnumerator LoopGet(string[] results, int numResults)
    {
        for (int i = 0; i < numResults; i++)
        {
            yield return new WaitForSeconds(0.1f);

            //TODO: Creo que no funciona, se pasa por valor creo
            StartCoroutine(GetCoroutine(results[i]));
        }
    }

    IEnumerator GetCoroutine(string result)
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
            result = www.downloadHandler.text;

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
