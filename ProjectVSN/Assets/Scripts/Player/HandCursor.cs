using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandCursor : MonoBehaviour
{

    public Camera cam; //main camera
    public Transform m_target; //object target
    private Vector3 targetWTSP; // vector 3d to word to screen point

    void Start()
    {
        targetWTSP = cam.WorldToScreenPoint(m_target.position);
    }

    void Update()
    {
        Vector3 mouseVec3 = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetWTSP.z));
        m_target.position = mouseVec3; //position to the object target
    }
}
