using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSearch : MonoBehaviour
{
    [SerializeField]
    SearchController sc;

    private void OnMouseUpAsButton()
    {
        sc.StartSearch();
    }
}
