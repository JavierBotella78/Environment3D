using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class LoginTests
{
    [Test]
    public void CorrectEraseTest()
    {
        // Registramos el usuario test en el txt de login
        LoginController.RegisterUser("test", "test");

        // Limpiamos el usuario test 
        Assert.AreEqual(true, LoginController.EraseUser("test", "test"));
    }

    [Test]
    public void FailEraseTest()
    {
        // Limpiamos el usuario test 
        Assert.AreEqual(false, LoginController.EraseUser("test", "test"));
    }

    [Test]
    public void CorrectRegisterTest()
    {
        // Registramos el usuario test en el txt de login
        Assert.AreEqual(true, LoginController.RegisterUser("test", "test"));

        // Limpiamos el usuario test 
        LoginController.EraseUser("test","test");
    }

    [Test]
    public void FailRegisterTest()
    {
        LoginController.RegisterUser("test", "test");

        // Registramos el usuario test en el txt de login
        Assert.AreEqual(false, LoginController.RegisterUser("test", "test"));

        // Limpiamos el usuario test 
        LoginController.EraseUser("test", "test");
    }

    [Test]
    public void CorrectLoginTest()
    {
        // Registramos el usuario si no está en el login
        LoginController.RegisterUser("test", "test");

        // Intentamos hacer login con el mismo usuario
        Assert.AreEqual(true, LoginController.CheckUser("test", "test"));

        // Limpiamos el usuario test
        LoginController.EraseUser("test", "test");
    }

    [Test]
    public void FailLoginTest()
    {
        // Intentamos hacer login con el mismo usuario
        Assert.AreEqual(false, LoginController.CheckUser("test", "test"));

        // Limpiamos el usuario test
        LoginController.EraseUser("test", "test");
    }

}
