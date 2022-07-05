using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConverterController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
        string test = System.IO.File.ReadAllText(@"C:\Users\USUARIO\Desktop\test.json");
        TextToVSNAssets(test, 10);
        */
    }

    public JObject TextToJSON(string text)
    {
        JObject jsonObj = JObject.Parse(text);

        return jsonObj;
    }

    public VSNAsset[] JSONObjToVSNAssets(JObject jsonObj, int num)
    {
        // JSON > response > docs[] > asset

        // Creamos la lista de assets
        VSNAsset[] vsnAssets = new VSNAsset[num];

        // Buscamos lo necesario para crear un VSNAsset
        int i = 0;
        foreach (var child in jsonObj.GetValue("response").Value<JToken>("docs").Children()) 
        {
            if (i == num)
                break;

            string id = child.Value<string>("Asset_ID_SORT");
            string name = child.Value<string>("Asset_TITLE_SORT");
            string imgurl = child.Value<string>("Asset_ICON");

            string desc = child.Value<string>("Asset_DESCRIPTION_SORT");
            string type = child.Value<string>("Asset_TYPE_NAME_REPRESENTATIVE");
            string classs = child.Value<string>("Asset_CLASS_NAME_REPRESENTATIVE");
            string lastDate = child.Value<string>("Asset_LAST_DATE");

            // Creamos un VSNAsset dentro del array
            vsnAssets[i] = new VSNAsset(id, name, imgurl, desc, type, classs, lastDate);

            i++;
        }


        return vsnAssets;
    }

    public VSNAsset[] TextToVSNAssets(string text, int num)
    {
        return JSONObjToVSNAssets(TextToJSON(text), num);
    }
}
