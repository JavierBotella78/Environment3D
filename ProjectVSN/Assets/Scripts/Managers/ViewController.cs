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
    private float padding = 1.0f;

    [SerializeField]
    private float distanceView1 = 2.6f;
    [SerializeField]
    private float distanceView2 = 4.5f;
    [SerializeField]
    private float distanceView3 = 2.6f;

    [SerializeField, Range(1, 10)]
    private int resultCount = 10;

    [SerializeField, Range(1, 2)]
    private int resultRows = 1;

    private int totalCount = 1;

    private int actualPage_ = 0;
    private int maxPage_ = 1;

    [SerializeField]
    private GameObject placeholderView1;
    [SerializeField]
    private GameObject placeholderView2;
    [SerializeField]
    private GameObject placeholderView3;
    [SerializeField]
    private GameObject placeholderOpen;

    private GameObject[] listPH;
    private GameObject actualResult = null;

    [SerializeField]
    private GameObject[] listViews;

    private GameObject actualView = null;

    [SerializeField]
    private GameObject view1;
    [SerializeField]
    private GameObject view2;
    [SerializeField]
    private GameObject view3;
    [SerializeField]
    private GameObject search;

    [SerializeField]
    private GameObject buttons;

    [SerializeField]
    private GameObject pagText; 

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

        //totalCount = resultCount * resultRows;
        totalCount = sc.getNumResuts();
        actualPage_ = sc.getNumPag() / totalCount;
        


        // Nos suscribimos al evento OnSearchEnded
        SearchController.OnSearchEnded += ShowAssets;
    }
    private void OnDestroy()
    {
        // Nos desuscribimos del evento OnSearchEnded
        SearchController.OnSearchEnded -= ShowAssets;
    }

    // Cuando acaba la busqueda o cambiamos la vista, aplicamos los cambios a la vista actual.
    public void ShowAssets(VSNAsset[] assets)
    {
        bool view1 = true;
        bool view2 = false;

        if (actualView.name == "View2")
        {
            view1 = false;
            view2 = true;
        }
        else if (actualView.name == "View3")
        {
            view1 = false;
            view2 = false;
        }

        // Iniciamos paginacion
        maxPage_ = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(sc.getTotalAssets()) / Convert.ToDouble(totalCount)));
        setPageText();

        int j = 0;

        // Asociamos a cada placeholder de la vista un asset distinto
        for (int i = 0; i < listPH.Length; i++)
        {
            if (j >= assets.Length || j >= totalCount || assets[j]==null)
            {
                Destroy(listPH[i].gameObject);
            }
            else
            {
                //guardamos asset en cada objeto para acceder a sus datos al abrir el resultado
                GameObject ph = listPH[i];
                ph.GetComponent<ClickRes>().setAsset(assets[j]);

                // Primary Key (No visible)
                TextMeshPro pkGO = ph.transform.Find("Pk").gameObject.GetComponent<TextMeshPro>();
                pkGO.text = assets[j].PK_;

                // Titulo
                TextMeshPro titleGO = ph.transform.Find("Title").gameObject.GetComponent<TextMeshPro>();
                titleGO.text = assets[j].Name_;

                // Img
                if (!String.IsNullOrWhiteSpace(assets[j].ImgURL_) && !String.IsNullOrEmpty(assets[j].ImgURL_))
                {
                    Renderer ren = ph.transform.Find("Img").gameObject.GetComponent<Renderer>();
                    ren.material.SetTexture("_MainTex", assets[j].ImgTexture_);
                }

                // Id
                TextMeshPro idGO = ph.transform.Find("Id").gameObject.GetComponent<TextMeshPro>();
                idGO.text = assets[j].Id_;

                // Tipo
                TextMeshPro typeGO = ph.transform.Find("Type").gameObject.GetComponent<TextMeshPro>();
                typeGO.text = assets[j].Type_;

                // Clase
                TextMeshPro classGO = ph.transform.Find("Class").gameObject.GetComponent<TextMeshPro>();
                classGO.text = assets[j].Class_;

                if (!view1 && view2)
                {
                    // Descripcion
                    TextMeshPro descGO = ph.transform.Find("Desc").gameObject.GetComponent<TextMeshPro>();
                    descGO.text = assets[j].Desc_;

                    // Ultima fecha de modificacion
                    TextMeshPro lastDate = ph.transform.Find("LastDate").gameObject.GetComponent<TextMeshPro>();
                    lastDate.text = assets[j].LastDate_;
                }
            }
            j++;
        }
    }

    // Se vuelve a la busqueda inicial. Vista especial sin placeholders
    public void ReturnToSearch()
    {
        actualView.SetActive(false);

        actualView = search;

        buttons.SetActive(false);

        // Movemos al jugador al punto "spawn" de la vista
        player.transform.position = actualView.transform.Find("Spawn").transform.position;

        OnViewChanged();

        actualView.SetActive(true);
        sc.setNumPag(0);
        actualPage_ = 0;
    }

    // Cambiamos entre la vista 1 o 2, y aplicamos los cambios necesarios
    public void ChangeView()
    {
        actualView.SetActive(false);

        if (actualView == view1)
            actualView = view2;
        else if (actualView == view2)
            actualView = view3;
        else
        {
            actualView = view1;
            buttons.SetActive(true);
        }

        actualView.SetActive(true);

        // Movemos al jugador al punto "spawn" de la vista
        player.transform.position = actualView.transform.Find("Spawn").transform.position;

        OnViewChanged();

        // Iniciamos los placeholders de la vista
        InitActualView();

        // Iniciamos paginacion
        setPageText();

        // Si tenemos los resultados listos, forzamos a que se asocien a los placeholders
        sc.ForceCallback();
    }

    private void InitActualView()
    {
        if (actualView && actualView.name != "Search")
        {
            // Encontramos los resultados de la vista
            Transform results = actualView.transform.Find("Results");
            results.rotation = Quaternion.Euler(0, 0, 0);


            // Destruimos los placeholder si los tuviera
            foreach (Transform child in results)
                Destroy(child.gameObject);

            // Iniciamos los placeholders 
            switch (actualView.name)
            {
                case "View1":
                    InitView1(results);
                    break;
                case "View2":
                    InitView2(results);
                    results.rotation = Quaternion.Euler(0, 180, 0);
                    break;
                case "View3":
                    InitView3(results);
                    break;
            }
        }
    }

    void InitPlaceholders(float distance, int rotationOffset, GameObject placeholder, Transform results)
    {

        // Segun el número de filas y columnas, iniciamos el array de placeholders
        listPH = new GameObject[resultCount * resultRows];

        // Separacion en grados entre cada placeholder
        float gradeSeparation = TOTALGRADES / (float)resultCount;

        for (int j = 0; j < resultRows; j++)
        {
            float tmpPadding = padding;

            if (resultRows == 1)
                tmpPadding = 0;
            else
                if (j % 2 != 0)
                tmpPadding *= -1;


            for (int i = 0; i < resultCount; i++)
            {
                if (actualView != view3)
                {
                    float actualGrade = -i * gradeSeparation - 180;
                    float radGrade = actualGrade * SEXAG2RAD;
                    float tempx = Mathf.Sin(radGrade);
                    float tempz = Mathf.Cos(radGrade);

                    Quaternion tempQuat = Quaternion.Euler(90, 0, -actualGrade + rotationOffset);
                    
                    // Instanciamos el placeholder correspondiente
                    listPH[resultCount * j + i] = Instantiate(placeholder, results);
                    listPH[resultCount * j + i].transform.rotation = tempQuat;
                    listPH[resultCount * j + i].transform.Translate(new Vector3(tempx * distance, tmpPadding, tempz * distance), results);
                }
                else
                {
                    float tempy=1.75f-i*0.4f;
                    // Instanciamos el placeholder correspondiente
                    listPH[resultCount * j + i] = Instantiate(placeholder, results);
                    listPH[resultCount * j + i].transform.Translate(new Vector3(tmpPadding*-distance, tempy, -0.2f), results);
                }
            }
        }

    }

    public void OpenResult(GameObject point, GameObject original)
    {
        if (actualResult == null)
        {
            actualResult = Instantiate(placeholderOpen, point.transform);

            VSNAsset actualAsset = original.GetComponent<ClickRes>().getAsset();

            // Primary Key (No visible)
            TextMeshPro pkGO = actualResult.transform.Find("Pk").gameObject.GetComponent<TextMeshPro>();
            pkGO.text = actualAsset.PK_;

            // Titulo
            TextMeshPro titleGO = actualResult.transform.Find("Title").gameObject.GetComponent<TextMeshPro>();
            titleGO.text = actualAsset.Name_;

            // Img
            if (!String.IsNullOrWhiteSpace(actualAsset.ImgURL_) && !String.IsNullOrEmpty(actualAsset.ImgURL_))
            {
                Renderer ren = actualResult.transform.Find("Img").gameObject.GetComponent<Renderer>();
                ren.material.SetTexture("_MainTex", actualAsset.ImgTexture_);
            }

            // Tipo
            TextMeshPro typeGO = actualResult.transform.Find("Type").gameObject.GetComponent<TextMeshPro>();
            typeGO.text = actualAsset.Type_;

            // Clase
            TextMeshPro classGO = actualResult.transform.Find("Class").gameObject.GetComponent<TextMeshPro>();
            classGO.text = actualAsset.Class_;

            // Id
            TextMeshPro idGO = actualResult.transform.Find("Id").gameObject.GetComponent<TextMeshPro>();
            idGO.text = actualAsset.Id_;

            // Descripcion
            TextMeshPro descGO = actualResult.transform.Find("Desc").gameObject.GetComponent<TextMeshPro>();
            descGO.text = actualAsset.Desc_;

            // Ultima fecha de modificacion
            TextMeshPro lastDate = actualResult.transform.Find("LastDate").gameObject.GetComponent<TextMeshPro>();
            lastDate.text = actualAsset.LastDate_;
        }
    }

    public void CloseResult()
    {
        Debug.Log("Cierro");
        Destroy(actualResult);
    }

    private void InitView1(Transform results)
    {
        InitPlaceholders(distanceView1, 0, placeholderView1, results);
    }

    private void InitView2(Transform results)
    {
        InitPlaceholders(distanceView2, -180, placeholderView2, results);
    }

    private void InitView3(Transform results)
    {
        InitPlaceholders(distanceView3, 0, placeholderView3, results);
    }

    public void AvPage()
    {
        if (actualPage_ + 1 < maxPage_)
        {
            actualPage_++;
            InitActualView();
            setPageText();
            sc.setNumPag(sc.getNumPag() + totalCount);
            sc.StartSearch();
            //sc.ForceCallback();
        }
    }

    public void RePage()
    {
        if (actualPage_ - 1 >= 0)
        {
            actualPage_--;
            InitActualView();
            setPageText();
            sc.setNumPag(sc.getNumPag() - totalCount);
            sc.StartSearch();
            //sc.ForceCallback();
        }
    }

    private void setPageText()
    {
        pagText.gameObject.GetComponent<TextMeshPro>().text=(actualPage_+1)+" to "+maxPage_;
    }

}
