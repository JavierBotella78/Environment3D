using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class NetworkingTests
{
    private NetworkController GetNetController() 
    {
        GameObject tmp = new();

        return tmp.AddComponent<NetworkController>();
    }

    [UnityTest]
    public IEnumerator CorrectGetTest()
    {
        NetworkController netManager = GetNetController();
        NetworkResponse result = new();
        
        yield return netManager.GetCoroutine(result, "https://catfact.ninja/fact");

        Assert.AreEqual(UnityWebRequest.Result.Success, result.resultCode);
    }

    [UnityTest]
    public IEnumerator FailGetTest()
    {
        NetworkController netManager = GetNetController();
        NetworkResponse result = new();

        yield return netManager.GetCoroutine(result, "url/no/valida");

        Assert.AreNotEqual(UnityWebRequest.Result.Success, result.resultCode);
    }

    // SE NECESITA ACCESO A LA VPN, AL CONTRARIO NO FUNCIONARÁ
    [UnityTest]
    public IEnumerator CorrectPostTest()
    {
        NetworkController netManager = GetNetController();
        NetworkResponse result = new();

        yield return netManager.PostCoroutine(result, @"https://testing/MAM/Searches/advanced/?start=0&maxrows=10", "test");

        Assert.AreEqual(UnityWebRequest.Result.Success, result.resultCode);
    }

    [UnityTest]
    public IEnumerator FailPostTest()
    {
        NetworkController netManager = GetNetController();
        NetworkResponse result = new();

        yield return netManager.PostCoroutine(result, "url/no/valida", "test");

        Assert.AreNotEqual(UnityWebRequest.Result.Success, result.resultCode);
    }
}
