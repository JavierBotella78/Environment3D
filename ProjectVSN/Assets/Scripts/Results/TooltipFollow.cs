using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipFollow : MonoBehaviour
{
    void Update()
    {
        var pos = gameObject.transform.position;
        // TODO: MIRAR EL MAXIMO Y MINIMO EN LA PANTALLA, OBTENER ESE NUMERO, Y DEPENDIENDO DE SI ESTÁ ARRIBA O ABAJO, PONERLE UN OFFSET AL TOOLTIP
        gameObject.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, pos.z);
    }
}
