using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CilinderResultsController : MonoBehaviour
{
    // CONSTANTS
    //
    const float TOTALGRADES = 360;

    const float SEXAG2RAD = Mathf.PI / 180f;


    // VARIABLES
    //
    [SerializeField]
    private float distance = 2.6f;

    [SerializeField]
    private int resultCount = 10;

    [SerializeField]
    private bool insideOutside = true;

    private int insideValue = -180;

    public GameObject placeholder;
    private GameObject[] listPH;

    private float gradeSeparation;

    // FUNCTIONS
    //

    // Start is called before the first frame update
    void Start()
    {
        insideValue = 0;
        if (insideOutside)
            insideValue = -180;

        initPlaceholders();
    }

    void initPlaceholders()
    {
        listPH = new GameObject[resultCount];
        gradeSeparation = TOTALGRADES / (float)resultCount;

        for (int i = 0; i < resultCount; i++)
        {
            float actualGrade = -i * gradeSeparation - 180;
            float radGrade = actualGrade * SEXAG2RAD;
            float tempx = Mathf.Sin(radGrade);
            float tempz = Mathf.Cos(radGrade);

            Quaternion tempQuat = Quaternion.Euler(90, 0, -actualGrade + insideValue);

            listPH[i] = Instantiate(placeholder, gameObject.transform);
            listPH[i].name += i; 
            listPH[i].transform.rotation = tempQuat;
            listPH[i].transform.Translate(new Vector3(tempx * distance, 0, tempz*distance), gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
