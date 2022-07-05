using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ViewController : MonoBehaviour
{
    // CONSTANTS
    //
    const float TOTALGRADES = 360;

    const float SEXAG2RAD = Mathf.PI / 180f;


    // VARIABLES
    //
    [SerializeField]
    private float distanceView1 = 2.6f;
    [SerializeField]
    private float distanceView2 = 4.5f;

    [SerializeField, Range(1, 10)]
    private int resultCount = 10;

    [SerializeField]
    private GameObject placeholderView1;
    [SerializeField]
    private GameObject placeholderView2;

    private GameObject[] listPH;

    [SerializeField]
    private GameObject[] listViews;

    private GameObject actualView = null;


    // EVENTS
    // 
    public delegate void ChangeCallback();
    public static event ChangeCallback OnViewChanged;


    // METHODS
    //
    private void Start()
    {
        listViews = GameObject.FindGameObjectsWithTag("View");

        if (listViews.Length != 0)
            actualView = Array.Find(listViews, element => element.name == "Search");

        SearchController.OnSearchEnded += ShowAssets;
    }
    private void OnDestroy()
    {
        SearchController.OnSearchEnded -= ShowAssets;
    }

    public void ShowAssets(VSNAsset[] assets)
    {
        int i = 0;

        foreach (var ph in listPH)
        {
            if (i == assets.Length)
                break;

            // Titulo
            TextMeshPro titleGO = ph.transform.Find("Title").gameObject.GetComponent<TextMeshPro>();
            titleGO.text = assets[i].Name_;

            // Img

            // Tipo
            TextMeshPro typeGO = ph.transform.Find("Type").gameObject.GetComponent<TextMeshPro>();
            typeGO.text = assets[i].Type_;

            // Clase
            TextMeshPro classGO = ph.transform.Find("Class").gameObject.GetComponent<TextMeshPro>();
            classGO.text = assets[i].Class_;

            i++;
        }
    }

    public void ChangeView(GameObject newView, GameObject oldView)
    {
        if (Array.Exists(listViews, element => element == newView))
        {
            actualView = newView;

            oldView.SetActive(false);
            actualView.SetActive(true);

            InitActualView();
        }
    }

    private void InitActualView()
    {
        if (actualView && actualView.name != "Search")
        {
            Transform results = actualView.transform.Find("Results");
            results.rotation = Quaternion.Euler(0, 0, 0);

            foreach (Transform child in results)
                Destroy(child.gameObject);

            switch (actualView.name)
            {
                case "View1":
                    InitView1(results);
                    break;
                case "View2":
                    InitView2(results);
                    break;
            }
        }
    }

    void InitPlaceholders(float distance, int rotationOffset, GameObject placeholder, Transform results)
    {

        listPH = new GameObject[resultCount];
        float gradeSeparation = TOTALGRADES / (float)resultCount;

        for (int i = 0; i < resultCount; i++)
        {
            float actualGrade = -i * gradeSeparation - 180;
            float radGrade = actualGrade * SEXAG2RAD;
            float tempx = Mathf.Sin(radGrade);
            float tempz = Mathf.Cos(radGrade);

            Quaternion tempQuat = Quaternion.Euler(90, 0, -actualGrade + rotationOffset);

            listPH[i] = Instantiate(placeholder, results);
            listPH[i].transform.rotation = tempQuat;
            listPH[i].transform.Translate(new Vector3(tempx * distance, 0, tempz * distance), results);
        }
    }

    private void InitView1(Transform results)
    {
        InitPlaceholders(distanceView1, 0, placeholderView1, results);
    }

    private void InitView2(Transform results)
    {
        InitPlaceholders(distanceView2, -180, placeholderView2, results);
    }

}