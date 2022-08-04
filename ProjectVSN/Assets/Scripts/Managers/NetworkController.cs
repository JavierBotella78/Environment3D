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
    public IEnumerator GetCoroutine(NetworkResponse result, string url)
    {
        // Se crea un objeto capaz de realizar llamadas GET a la url indicada
        UnityWebRequest www = UnityWebRequest.Get(url); 


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

     private const string prePayload = "{\"ID\":0,\"Name\":\"\",\"Completed\":true,\"Private\":false,\"Class\":\"\",\"AssetType\":\"\",\"Search\":{\"requerimientosBusqueda\":{\"id\":\"0a739c06-406c-4abd-8ffa-cf9cc7aed142\",\"tipoAgrupacion\":\"0\",\"ItemsBusquedaReglas\":[],\"ItemsBusquedaGrupos\":[{\"id\":\"bda6a22f-1705-4cde-92e3-de18bb12329b\",\"tipoAgrupacion\":\"0\",\"ItemsBusquedaReglas\":[{\"campo\":{\"id\":\"\",\"descripcion\":\"\",\"tipoCampo\":0,\"dataSource\":null},\"condicion\":{\"id\":18,\"descripcion\":\"AllTexts\"},\"valor\":\"";
    private const string postPayload = "\",\"searchMode\":\"1\"}],\"ItemsBusquedaGrupos\":[]}]},\"sortFieldsList\":[{\"solrSortField\":\"LAST_DATE_MDT_Asset_System\",\"sortDirection\":\"1\"}]}}";

    public IEnumerator PostCoroutine(NetworkResponse result, string url, string searchText)
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

    public IEnumerator GetTexture(string url, VSNAsset asset)
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
