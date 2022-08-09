using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphController : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabAsset;

    [SerializeField]
    private GameObject parentAssets;

    private GameObject[] parentTypes;

    public int numOfRelations = 10;
    public int typesOfRelations = 4;

    public float hPadding = 1.0f;
    public float vPadding = 1.2f;

    public float yOffset = 1.0f;

    public int assetsPerLine = 4;

    public float distanceFromMainAsset = 5.0f;


    public void StartGeneration()
    {
        for (int i = 0; i < parentAssets.transform.childCount; ++i)
            Destroy(parentAssets.transform.GetChild(i).gameObject);

        float acumulativeGrades = 0.0f;
        float gradeOffset = 360.0f / typesOfRelations;
        float gradeOffsetRadians = gradeOffset * Mathf.PI / 180.0f;

        parentTypes = new GameObject[typesOfRelations];

        for (int i = 0; i < typesOfRelations; ++i)
        {
            parentTypes[i] = new GameObject();
            parentTypes[i].transform.SetParent(parentAssets.transform);

            float x = Mathf.Cos(acumulativeGrades) * distanceFromMainAsset;
            float y = Mathf.Sin(acumulativeGrades) * distanceFromMainAsset;

            Vector3 tmpIniPos = new Vector3(x, y + yOffset, 0);

            GroupGeneration(tmpIniPos, parentTypes[i].transform, GetColorByType(i));

            acumulativeGrades += gradeOffsetRadians;
        }
    }

    public void GroupGeneration(Vector3 iniPos, Transform parent, Color color)
    {
        int lines = numOfRelations / assetsPerLine;
        int accumulatedAssets = 0;
        float halfHPadding = hPadding / 2.0f;

        for (int i = 0; i <= lines; ++i)
        {
            int tmp = numOfRelations - accumulatedAssets;
            int assetsNextLine = assetsPerLine;

            Debug.Log(tmp);

            if (tmp / assetsPerLine == 0 && tmp % assetsPerLine != 0)
                assetsNextLine -= assetsPerLine - (tmp % assetsPerLine);

            assetsNextLine--;

            Debug.Log(assetsNextLine);

            float lineYPos = iniPos.y - (i * vPadding);

            for (int j = 0; j < assetsPerLine && (i * assetsPerLine + j) < numOfRelations; ++j)
            {
                float assetxPos = iniPos.x + (assetsNextLine * -halfHPadding) + (j * hPadding);

                GameObject tmpObj1 = Instantiate(prefabAsset, parent);

                tmpObj1.transform.position = new Vector3(assetxPos, lineYPos, 0);

                var tmpObj2 = tmpObj1.transform.Find("Back").gameObject;

                tmpObj2.GetComponent<Renderer>().material.SetTexture("_MainTex", null);
                tmpObj2.GetComponent<Renderer>().material.SetColor("_Color", color);

                tmpObj2.GetComponent<Renderer>().material.color = color;

                accumulatedAssets++;
            }

        }
    }

    public Color GetColorByType(int type) 
    {

        Color ret;

        switch (type) 
        {
            case 0:
                ret = Color.red;
                break;
            case 1:
                ret = Color.blue;
                break;
            case 2:
                ret = Color.green;
                break;
            case 3:
                ret = Color.yellow;
                break;
            case 4:
                ret = Color.magenta;
                break;
            case 5:
                ret = Color.gray;
                break;
            default:
                ret = Color.cyan;
                break;
        }

        return ret;
    }
}
