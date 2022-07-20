using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePage : MonoBehaviour
{
    public ViewController vc;

    private void OnMouseUpAsButton()
    {
        vc.RePage();
    }
}
