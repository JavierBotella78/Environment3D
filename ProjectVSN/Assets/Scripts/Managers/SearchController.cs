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

    public static int imagesToSearch = 0;

    private bool imageSearchStarted = false;
    private bool imageSearchFinished = false;

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
        // https://catfact.ninja/fact
        // https://testing/MAM/Searches/advanced/?start=0&maxrows=30
        // https://testing/explorerservice/webpages/default.aspx#search=1092
        nr = nc.StartSearch(@"https://testing/MAM/Searches/advanced/?start=0&maxrows=30"); 
        searchStarted = true;
        SearchFinished = false;
        imageSearchStarted = false;
        imageSearchFinished = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (searchStarted && nr.resultCode == UnityWebRequest.Result.Success)
        {
            searchStarted = false;

            // string test = System.IO.File.ReadAllText(@"Assets/Files/test.json");

            Results = cc.TextToVSNAssets(nr.respText, 50);

            imageSearchStarted = true;

            foreach (var asset in Results)
            {
                if (asset != null && !String.IsNullOrWhiteSpace(asset.ImgURL_) && !String.IsNullOrEmpty(asset.ImgURL_))
                {
                    imagesToSearch++;
                    nc.StartImageSearch(asset);
                }
            }
        }

        if (imagesToSearch == 0 && imageSearchStarted)
        {
            imageSearchStarted = false;
            imageSearchFinished = true;
        }

        if (!SearchFinished && imageSearchFinished)
        {
            OnSearchEnded(Results);

            SearchFinished = true;
            searchStarted = false;
        }
    }

    public void ForceCallback() 
    {
        if(SearchFinished && Results != null)
            OnSearchEnded(Results);
    }


}
