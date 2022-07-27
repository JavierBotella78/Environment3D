using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConverterController
{

    // Transforma un texto que esté formateado como un json a un JSONObject
    public static JObject TextToJSON(string text)
    {
        JObject jsonObj = JObject.Parse(text);

        return jsonObj;
    }

    // Transforma un JSONObject a una lista de VSNAssets
    public static VSNAsset[] JSONObjToVSNAssets(JObject jsonObj, int num, ref int totalAssets)
    {
        // JSON > response > docs[] > asset

        // Creamos la lista de assets
        totalAssets = System.Convert.ToInt32(jsonObj.GetValue("response").Value<string>("numFound"));
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

    // Transforma un texto json a una lista de objetos VSNAsset
    public static VSNAsset[] TextToVSNAssets(string text, int num, ref int total)
    {
        return JSONObjToVSNAssets(TextToJSON(text), num, ref total);
    }

}
