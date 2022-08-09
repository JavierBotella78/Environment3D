using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGraphButton : MonoBehaviour
{
    [SerializeField]
    private GraphController gc;

    private void OnMouseUpAsButton() 
    {
        gc.StartGeneration();
    }
}
