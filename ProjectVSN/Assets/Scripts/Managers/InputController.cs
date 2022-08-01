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
    [SerializeField]
    private Animator animator;

    private void Start()
    {
        loginButton.onClick.AddListener(CheckLogin);
        registerButton.onClick.AddListener(RegisterUser);
    }

    public void CheckLogin()
    {
        bool login = LoginController.CheckUser(username.text, password.text);
        Debug.Log(login);

        if (login)
        {
            animator.SetBool("logtrue", true);
        }
        else
            animator.SetBool("logfalse", true);

    }

    public void RegisterUser() 
    {
        bool register = LoginController.RegisterUser(username.text, password.text);
        Debug.Log(register);

        if (register)
        {
            animator.SetBool("logtrue", true);
        }
        else
            animator.SetBool("logfalse", true);
    }
}
