using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// Certificado para los https
public class ForceAcceptAll : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}

public class NetworkController : MonoBehaviour
{
    const string ImageUrl = @"https://testing/MAM/WebPages/";

    [SerializeField]
    private bool isEnabled = false;

    // Con una url, empecamos una petición POST para conectar con la API de VSN
    public NetworkResponse StartSearch(string url, string searchText)
    {
        NetworkResponse result = new();

        if (isEnabled)
        {
            StartCoroutine(PostCoroutine(result, url, searchText));
        }

        return result;
    }

    // Con un Asset de VSN, recogemos el path de la imagen y la buscamos
    public void StartImageSearch(VSNAsset asset)
    {
        string url = ImageUrl + asset.ImgURL_;

        if (isEnabled)
        {
            StartCoroutine(GetTexture(url, asset));
        }
    }

    // "https://catfact.ninja/fact"
    IEnumerator GetCoroutine(NetworkResponse result, string url)
    {
        // Se crea un objeto capaz de realizar llamadas GET a la url indicada
        UnityWebRequest www = UnityWebRequest.Get(url); // Llamada de prueba que da una curiosidad sobre gatos aleatoria


        yield return www.SendWebRequest();


        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            result.resultCode = www.result;
        }
        else
        {
            // Mostrar el resultado como texto (json)
            result.respText = www.downloadHandler.text;
            result.resultCode = www.result;

        }

    }

    //private string payload = "{\"ID\":0,\"Name\":\"\",\"Completed\":true,\"Private\":false,\"Class\":\"\",\"AssetType\":\"\",\"Search\":{\"requerimientosBusqueda\":{\"id\":\"7f3e8fb1-6cfc-4ef5-b638-2c3f63dd4b7b\",\"tipoAgrupacion\":\"0\",\"ItemsBusquedaReglas\":[],\"ItemsBusquedaGrupos\":[{\"id\":\"b7031079-c3ca-465f-8af8-60636417718e\",\"tipoAgrupacion\":\"0\",\"ItemsBusquedaReglas\":[{\"campo\":{\"dataSource\":null,\"subtipos\":[{\"id\":\"18\",\"descripcion\":\"AllTexts\"}],\"valores\":[],\"id\":\"\",\"descripcion\":\"Quick search fields\",\"tipoCampo\":11},\"condicion\":{\"id\":\"18\",\"descripcion\":\"AllTexts\"},\"valor\":\"*\",\"id\":\"2933af79-b6c7-4a2c-90b5-51dde6d0ac82\",\"code\":\"\",\"searchMode\":\"1\"}],\"ItemsBusquedaGrupos\":[]}]},\"sortFieldsList\":[{\"solrSortField\":\"LAST_DATE_MDT_Asset_System\",\"sortDirection\":\"1\"}]}}";
    //private string payload = "{\"ID\":0,\"Name\":\"\",\"Completed\":true,\"Private\":false,\"Class\":\"\",\"AssetType\":\"\",\"Search\":{\"requerimientosBusqueda\":{\"id\":\"a2ac0663-9198-4e98-89e7-787f60f7c357\",\"tipoAgrupacion\":\"0\",\"ItemsBusquedaReglas\":[],\"ItemsBusquedaGrupos\":[{\"id\":\"d54ac8a7-3e87-4c31-ae68-5f81070276b3\",\"tipoAgrupacion\":\"0\",\"ItemsBusquedaReglas\":[{\"campo\":{\"id\":\"\",\"descripcion\":\"\",\"tipoCampo\":0,\"dataSource\":null},\"condicion\":{\"id\":18,\"descripcion\":\"AllTexts\"},\"valor\":\"hola\",\"searchMode\":\"1\"}],\"ItemsBusquedaGrupos\":[]}]},\"sortFieldsList\":[{\"solrSortField\":\"LAST_DATE_MDT_Asset_System\",\"sortDirection\":\"1\"}]}}";
    //private string busqueda = "*";
    private string prePayload = "{\"ID\":0,\"Name\":\"\",\"Completed\":true,\"Private\":false,\"Class\":\"\",\"AssetType\":\"\",\"Search\":{\"requerimientosBusqueda\":{\"id\":\"0a739c06-406c-4abd-8ffa-cf9cc7aed142\",\"tipoAgrupacion\":\"0\",\"ItemsBusquedaReglas\":[],\"ItemsBusquedaGrupos\":[{\"id\":\"bda6a22f-1705-4cde-92e3-de18bb12329b\",\"tipoAgrupacion\":\"0\",\"ItemsBusquedaReglas\":[{\"campo\":{\"id\":\"\",\"descripcion\":\"\",\"tipoCampo\":0,\"dataSource\":null},\"condicion\":{\"id\":18,\"descripcion\":\"AllTexts\"},\"valor\":\"";
    private string postPayload = "\",\"searchMode\":\"1\"}],\"ItemsBusquedaGrupos\":[]}]},\"sortFieldsList\":[{\"solrSortField\":\"LAST_DATE_MDT_Asset_System\",\"sortDirection\":\"1\"}]}}";

    IEnumerator PostCoroutine(NetworkResponse result, string url, string searchText)
    {
        string payload = prePayload + searchText + postPayload;
        // Se crea un objeto capaz de realizar llamadas POST a la url indicada
        UnityWebRequest www = UnityWebRequest.Put(url, payload);

        // Ponemos los atributos necesarios en la cabecera
        www.method = "POST";
        //www.SetRequestHeader("Host"             , "testing");
        //www.SetRequestHeader("Content-Length"   , "578");
        www.SetRequestHeader("Accept"           , "application/json, text/javascript, */*; q=0.01");
        www.SetRequestHeader("Content-type"     , "application/json; charset=UTF-8");
        www.SetRequestHeader("Authorization"    , "Logon rbaena:Vsn1234");
        www.SetRequestHeader("Accept-encoding"  , "gzip, deflate, br");
        www.SetRequestHeader("Accept-language"  , "es-ES,es;q=0.9");

        // Aplicamos el certificado
        var cert = new ForceAcceptAll();
        www.certificateHandler = cert;

        yield return www.SendWebRequest();


        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            result.resultCode = www.result;
        }
        else
        {
            // Mostrar el resultado como texto (json)
            result.respText = www.downloadHandler.text;
            result.resultCode = www.result;
        }

        cert?.Dispose();

    }

    IEnumerator GetTexture(string url, VSNAsset asset)
    {
        // Se crea un objeto capaz de realizar llamadas GET a la url indicada
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);

        var cert = new ForceAcceptAll();
        www.certificateHandler = cert;

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Descargamos las imagenes y las transformamos en texturas
            asset.ImgTexture_ = DownloadHandlerTexture.GetContent(www);
            SearchController.imagesToSearch--;
        }

        cert?.Dispose();
    }


}
