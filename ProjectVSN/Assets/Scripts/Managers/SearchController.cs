using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SearchController : MonoBehaviour
{
    // VARIABLES
    //

    public VSNAsset[] Results { get; set; }

    [SerializeField]
    private NetworkController nc;
    private NetworkResponse nr;

    [SerializeField]
    private ConverterController cc;

    private bool searchStarted = false;
    public bool SearchFinished { get; set; }

    [SerializeField]
    private int numResults = 50;

    public static int imagesToSearch = 0;

    private bool imageSearchStarted = false;
    private bool imageSearchFinished = false;

    [SerializeField]
    private GameObject inputSearch = null;
    private string searchText = "*";

    // EVENTS
    //
    public delegate void SearchEndedCallback(VSNAsset[] assets);
    public static event SearchEndedCallback OnSearchEnded;

    // METHODS
    //

    private void Start()
    {
        SearchFinished = false;
    }

    // Al empezar la busqueda, usamos el NetworkController e inicializamos las variables
    public void StartSearch()
    {
        //obtenemos texto de busqueda
        if (inputSearch)
        {
            string newText = inputSearch.GetComponent<TMPro.TMP_InputField>().text;
            if (newText != "")
                searchText = newText;
            else
                searchText = "*";
        }

        Results = null;
        nr = new NetworkResponse();
        // https://catfact.ninja/fact
        // "https://testing/MAM/Searches/advanced/?start=0&maxrows=30" + numResults
        // https://testing/explorerservice/webpages/default.aspx#search=1092
        nr = nc.StartSearch(@"https://testing/MAM/Searches/advanced/?start=0&maxrows=" + numResults, searchText); 
        searchStarted = true;
        SearchFinished = false;
        imageSearchStarted = false;
        imageSearchFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Si ha terminado la búsqueda desde el NetworkController, entramos
        if (searchStarted && nr.resultCode == UnityWebRequest.Result.Success)
        {
            searchStarted = false;

            // string test = System.IO.File.ReadAllText(@"Assets/Files/test.json");

            // Transformamos la respuesta a una lista de VSNAsset
            Results = cc.TextToVSNAssets(nr.respText, 50);

            imageSearchStarted = true;

            // Por cada VSNAsset, buscamos si tiene una imagen con el NetworkController
            foreach (var asset in Results)
            {
                if (asset != null && !String.IsNullOrWhiteSpace(asset.ImgURL_) && !String.IsNullOrEmpty(asset.ImgURL_))
                {
                    imagesToSearch++;
                    nc.StartImageSearch(asset);
                }
            }
        }

        // Si ha acabado la busqueda de imagenes, cambiamos las variables
        if (imagesToSearch == 0 && imageSearchStarted)
        {
            imageSearchStarted = false;
            imageSearchFinished = true;
        }

        // Si no ha acabado la busqueda total y ha acabado la busqueda de imagenes, aplicamos los cambias a la vista
        if (!SearchFinished && imageSearchFinished)
        {
            OnSearchEnded(Results);

            SearchFinished = true;
            searchStarted = false;
        }
    }

    // Se llama al evento OnSearchEnded() de forma forzada, para recoger los resultados.
    public void ForceCallback() 
    {
        if(SearchFinished && Results != null)
            OnSearchEnded(Results);
    }


}
