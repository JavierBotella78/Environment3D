using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetController : MonoBehaviour
{
    public ExtendedVSNAsset mainAsset;

    public NetworkController nc;

    private NetworkResponse netResponse;

    bool check = false;

    private void Start()
    {
        string assetPk = GameObject.Find("SelectedAsset").GetComponent<SelectedAssetController>().selectedAssetPK;

         netResponse =  nc.StartGET(@"https://testing/MAM/assets/" + assetPk + "?");
    }

    private void Update()
    {
        
        if (netResponse.resultCode == UnityWebRequest.Result.Success && !check)
        {


            Debug.Log(netResponse.respText);
            check = true;
        }
        
    }
}
