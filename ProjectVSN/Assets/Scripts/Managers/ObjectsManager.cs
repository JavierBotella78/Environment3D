using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{
    private GameObject openObject = null;

    public bool openViewObject(GameObject vista)
    {
        if (openObject == null || openObject != vista)
        {
            //cierro vista anterior
            if (openObject)
                openObject.GetComponent<ClickObject>().closeView();
            //asigno vista actual
            openObject = vista;
            return true;
        }
        return false;
    }
}
