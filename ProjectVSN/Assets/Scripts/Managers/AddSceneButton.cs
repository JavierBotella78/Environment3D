using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddSceneButton : MonoBehaviour
{
    [SerializeField]
    public string sceneName = "";

    private void OnMouseUpAsButton()
    {
        SceneManager.LoadScene(sceneName);
    }

}
