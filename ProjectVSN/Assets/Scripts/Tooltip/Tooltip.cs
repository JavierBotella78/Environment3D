using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI contentField;

    public LayoutElement layoutElement;

    public int characterWrapLimit;

    private void Update()
    {
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (contentLength > characterWrapLimit) ? true : false;
    }
}
