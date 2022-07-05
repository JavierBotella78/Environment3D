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


    // METHODS
    //

    private void Start()
    {
        StartSearch();
    }

    public void StartSearch()
    {
        nr = nc.StartSearch("https://catfact.ninja/fact");
        searchStarted = true;
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

            foreach (var asset in Results)
            {
                Debug.Log(asset);
            }

            //TODO: EVENTO PARA EL VIEWCONTROLLER
        }
    }


}
