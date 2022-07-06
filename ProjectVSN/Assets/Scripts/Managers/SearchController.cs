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

    [SerializeField]
    private bool searchStarted = false;

    public bool SearchFinished { get; set; }

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

    public void StartSearch()
    {
        Results = null;
        nr = new NetworkResponse();
        nr = nc.StartSearch("https://catfact.ninja/fact");
        searchStarted = true;
        SearchFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (searchStarted && nr.resultCode == UnityWebRequest.Result.Success)
        {
            searchStarted = false;

            //TODO: LLAMAR CON LA URL DE VSN

            Debug.Log(nr.respText);

            string test = System.IO.File.ReadAllText(@"C:\Users\USUARIO\Desktop\test.json");

            Results = cc.TextToVSNAssets(test, 10);

            //results = cc.TextToVSNAssets(nr.respText, 10);

            OnSearchEnded(Results);

            SearchFinished = true;
        }
    }

    public void ForceCallback() 
    {
        if(SearchFinished && Results != null)
            OnSearchEnded(Results);
    }


}
