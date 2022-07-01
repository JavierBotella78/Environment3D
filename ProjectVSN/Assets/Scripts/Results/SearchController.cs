using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchController : MonoBehaviour
{
    public GameObject[] results;

    NetworkController nc;


    public bool searchStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        ChangeView.OnSearchStart += StartSearch;

        nc = GameObject.Find("NetworkManager").GetComponent<NetworkController>();
    }
    private void OnDestroy()
    {
        ChangeView.OnSearchStart -= StartSearch;
    }


    private void StartSearch()
    {
        // Empezamos la búsqueda
        searchStarted = true;

        // Buscamos un elemento Resultado valido donde poner los resultados
        var tmpObjects = GameObject.FindGameObjectsWithTag("Result");
        foreach (var obj in tmpObjects)
        {
            if (obj.active)
            {
                results = obj.GetComponent<CilinderResultsController>().ListOfPlaceholders;
                break;
            }
        }

        // Buscamos con N resultados
        nc.StartSearch(10);
    }

    // Update is called once per frame
    void Update()
    {
        // Si hemos empezado a buscar...
        if (searchStarted)
        {
            // ...miramos que la corutina haya terminado
            if (Array.Exists(nc.results, element => element == "" || String.IsNullOrWhiteSpace(element)))
                return;

            searchStarted = false;

            //TODO: Leer json y dividir los textos
            foreach (string cadena in nc.results)
            {
                Debug.Log(cadena);
            }
        }
    }


}
