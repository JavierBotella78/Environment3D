using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetController : MonoBehaviour
{
    public VSNAsset mainAsset;

    public NetworkController nc;

    private NetworkResponse netResponse;

    bool check = false;

    private void Start()
    {
         netResponse =  nc.StartGET(@"https://testing/MAM/assets/" + mainAsset.PK_ + "?");
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
