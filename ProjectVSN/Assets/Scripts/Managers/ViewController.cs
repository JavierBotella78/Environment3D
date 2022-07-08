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
    private SearchController sc;

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

    [SerializeField]
    private GameObject view1;
    [SerializeField]
    private GameObject view2;
    [SerializeField]
    private GameObject search;

    [SerializeField]
    private Texture testTextu;


    [SerializeField]
    private GameObject[] buttons;

    [SerializeField]
    private GameObject player;


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
        bool view1 = true;

        if (actualView.name == "View2")
            view1 = false;


        foreach (var ph in listPH)
        {
            if (i == assets.Length)
                break;

            // Titulo
            TextMeshPro titleGO = ph.transform.Find("Title").gameObject.GetComponent<TextMeshPro>();
            titleGO.text = assets[i].Name_;

            // Img
            if (!String.IsNullOrWhiteSpace(assets[i].ImgURL_) && !String.IsNullOrEmpty(assets[i].ImgURL_))
            {

            }
            //Renderer ren = ph.transform.Find("Img").gameObject.GetComponent<Renderer>();
            //ren.material.SetTexture("_MainTex", testTextu);

            // Tipo
            TextMeshPro typeGO = ph.transform.Find("Type").gameObject.GetComponent<TextMeshPro>();
            typeGO.text = assets[i].Type_;

            // Clase
            TextMeshPro classGO = ph.transform.Find("Class").gameObject.GetComponent<TextMeshPro>();
            classGO.text = assets[i].Class_;

            if (!view1)
            {
                // Descripcion
                TextMeshPro descGO = ph.transform.Find("Desc").gameObject.GetComponent<TextMeshPro>();
                descGO.text = assets[i].Desc_;

                // Ultima fecha
                TextMeshPro lastDate = ph.transform.Find("LastDate").gameObject.GetComponent<TextMeshPro>();
                lastDate.text = assets[i].LastDate_;
            }

            i++;
        }

    }

    public void ReturnToSearch() 
    {
        actualView.SetActive(false);

        actualView = search;

        foreach (GameObject button in buttons) 
            button.SetActive(false);

        player.transform.position = actualView.transform.Find("Spawn").transform.position;

        actualView.SetActive(true);
    }

    public void ChangeView()
    {
        actualView.SetActive(false);

        if (actualView == view1)
            actualView = view2;
        else
        {
            actualView = view1;
            foreach (GameObject button in buttons)
                button.SetActive(true);
        }

        actualView.SetActive(true);

        player.transform.position = actualView.transform.Find("Spawn").transform.position;

        InitActualView();

        sc.ForceCallback();
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
