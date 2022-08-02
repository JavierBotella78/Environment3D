using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public static class LoginController
{
    const string loginPath = @"Assets/Files/login.txt";
    const string separator = "|||";
    static SHA256 encoder = SHA256.Create();

    /*
    private void Start()
    {
        encoder = SHA256.Create();
        Debug.Log(RegisterUser("rbaena", "Vsn1234"));
        Debug.Log(CheckUser("rbaena", "Vsn1234"));

    }
    */

    // Comprueba que el usuario exista, y si lo hace, comprueba que la contraseña codificada coincida
    public static bool CheckUser(string name, string pasw)
    {
        string[] lines = GetLoginFileLines();

        
        // Iteramos sobre todo el documento y comprobamos que el usuario exista
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
    public static bool RegisterUser(string user, string pasw)
    {
        if (!String.IsNullOrWhiteSpace(user) && !String.IsNullOrWhiteSpace(pasw))
        {
            string[] lines = GetLoginFileLines();

            // Comprobamos que el documento no esté vacio ni que el usuario ya exista
            if (lines.Length != 0)
            {
                foreach (string line in lines)
                {
                    string tmpName = line.Substring(0, line.IndexOf(separator));

                    if (tmpName == user)
                        return false;
                }
            }

            // Encriptamos la contraseña y almacenamos usuario y contraseña
            string encPasw = EncryptString(pasw);

            AddUser(user, encPasw);
        }

        return true;
    }

    // Elimina un usuario pero es necesaria la contraseña
    public static bool EraseUser(string user, string pasw)
    {
        // Comprobamos que el usuario exista
        if (!CheckUser(user, pasw))
            return false;

        string[] lines = GetLoginFileLines();
        string[] newLines = new string[lines.Length - 1];
        int i = 0;

        // Iteramos y añadimos a un nuevo string a todos menos al usuario que queremos eliminar
        foreach (string line in lines)
        {
            string tmpName = line.Substring(0, line.IndexOf(separator));

            if (tmpName != user)
            {
                newLines[i] = line;
                i++;
            }
        }

        SetLoginFileLines(newLines);

        return true;
    }

    // Encriptamos la cadena de caracteres con la funcion SHA256
    private static string EncryptString(string chain)
    {
        byte[] encBytes = encoder.ComputeHash(Encoding.UTF8.GetBytes(chain));
        string encChain = Convert.ToBase64String(encBytes);

        return encChain;
    }

    // Devuelve el contenido del login.txt
    private static string GetLoginFileContent()
    {
        return System.IO.File.ReadAllText(loginPath);
    }

    private static void SetLoginFileContent(string content)
    {
        System.IO.File.WriteAllText(loginPath, content, Encoding.UTF8);
    }

    // Devuelve el contenido del login.txt en forma de lineas string[]
    private static string[] GetLoginFileLines()
    {
        return System.IO.File.ReadAllLines(loginPath);
    }

    private static void SetLoginFileLines(string[] lines)
    {
        System.IO.File.WriteAllLines(loginPath, lines, Encoding.UTF8);
    }

    // Añade un nuevo usuario y su contraseña en el documento de texto
    private static void AddUser(string name, string encPass)
    {
        string fileContent = "";
        string offset = "\r\n";

        if (GetLoginFileLines().Length == 0)
            offset = "";
        
        fileContent += name;
        fileContent += separator;
        fileContent += encPass;
        fileContent += offset;

        System.IO.File.AppendAllText(loginPath, fileContent);
    }


}
