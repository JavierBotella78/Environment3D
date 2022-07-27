using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;


public class ConverterTests
{
    [Test]
    public void String2JsonTrue()
    {
        // Recibimos un string, el cual debe ser un JSON al transformarlo
        string test = System.IO.File.ReadAllText(@"Assets/Files/test.json");

        // Lo convertimos a un objeto JSON
        var json = ConverterController.TextToJSON(test);

        // Comprobamos el tipo
        Assert.AreEqual(json.GetType(), typeof(Newtonsoft.Json.Linq.JObject));

        var child = json.GetValue("response");

        // Obtenemos un hijo y comprobamos que no sea null
        Assert.AreNotEqual(child.ToString(), "");
    }

    [Test]
    public void String2VSNAsset() 
    {
        // Recibimos un string, el cual debe ser un JSON al transformarlo
        string test = System.IO.File.ReadAllText(@"Assets/Files/test.json");

        // Lo convertimos a un objeto VSNAsset[]
        int total = 0;
        var assets = ConverterController.TextToVSNAssets(test, 10, ref total);

        Assert.AreEqual(10, assets.Length);

        foreach (var asset in assets)
        {
            Assert.NotNull(asset);
            Assert.NotNull(asset.Id_);

            Assert.AreNotEqual("", asset.Id_);
        }
    }
}
