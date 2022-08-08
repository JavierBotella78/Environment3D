using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectAsset : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        Transform transAssetParent = gameObject.transform.parent;

        TextMeshPro pkGO = transAssetParent.Find("Pk").gameObject.GetComponent<TextMeshPro>();
        GameObject.Find("SelectedAsset").GetComponent<SelectedAssetController>().selectedAssetPK = pkGO.text;
    }
}
