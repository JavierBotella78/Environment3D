using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedAssetController : MonoBehaviour
{
    public string selectedAssetPK = "";

    void Start()
    {
        DontDestroyOnLoad(this);
    }



}
