using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    public Button loginButton;
    public Button registerButton;

    [SerializeField]
    private TMP_InputField username;
    [SerializeField]
    private TMP_InputField password;

    private void Start()
    {
        loginButton.onClick.AddListener(CheckLogin);
        registerButton.onClick.AddListener(RegisterUser);
    }

    public void CheckLogin()
    {
        Debug.Log(LoginController.CheckUser(username.text, password.text));
    }

    public void RegisterUser() 
    {
        Debug.Log(LoginController.RegisterUser(username.text, password.text));
    }
}
