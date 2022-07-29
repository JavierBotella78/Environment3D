using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class LoginController : MonoBehaviour
{
    const string loginPath = @"Assets/Files/login.txt";
    const string separator = "|||";
    SHA256 encoder;

    private void Start()
    {
        encoder = SHA256.Create();
        Debug.Log(RegisterUser("rbaena", "Vsn1234"));
        Debug.Log(CheckLogin("rbaena", "Vsn1234"));

    }

    // Comprueba que el usuario exista, y si lo hace, comprueba que la contraseña codificada coincida
    public bool CheckLogin(string name, string pasw)
    {
        string[] lines = GetLoginFileLines();

        foreach (string line in lines)
        {
            string tmpName = line.Substring(0, line.IndexOf(separator));

            if (tmpName == name)
            {
                string encPasw = EncryptString(pasw);
                string tmpEncPasw = line.Substring(line.IndexOf(separator) + 3);

                if (tmpEncPasw == encPasw)
                    return true;

                return false;
            }
        }

        return false;
    }

    // Si no existe ya el usuario, lo crea
    public bool RegisterUser(string user, string pasw)
    {
        if (!String.IsNullOrWhiteSpace(user) && !String.IsNullOrWhiteSpace(pasw))
        {
            string[] lines = GetLoginFileLines();

            if (lines.Length != 0)
            {
                foreach (string line in lines)
                {
                    string tmpName = line.Substring(0, line.IndexOf(separator));

                    if (tmpName == user)
                        return false;
                    else
                        break;
                }
            }

            string encPasw = EncryptString(pasw);

            AddUser(user, encPasw);
        }

        return true;
    }

    public string EncryptString(string chain)
    {
        string encChain = Encoding.UTF8.GetString(encoder.ComputeHash(Encoding.UTF8.GetBytes(chain)));

        return encChain;
    }

    private string GetLoginFileContent()
    {
        return System.IO.File.ReadAllText(loginPath);
    }

    private string[] GetLoginFileLines()
    {
        return System.IO.File.ReadAllLines(loginPath);
    }

    private void AddUser(string name, string encPass)
    {
        string fileContent = "";
        string offset = "\r\n";

        if (GetLoginFileLines().Length == 0)
            offset = "";

        fileContent += offset;
        fileContent += name;
        fileContent += separator;
        fileContent += encPass;

        System.IO.File.AppendAllText(loginPath, fileContent);
    }


}
