using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TooltipTitle : MonoBehaviour
{
    private TextMeshPro tooltipText;

    private void Start()
    {
        tooltipText = GameObject.FindGameObjectWithTag("Player").transform.Find("Tooltip").Find("TTText").gameObject.GetComponent<TextMeshPro>();
    }

    private void OnMouseOver()
    {
        tooltipText.text = gameObject.GetComponent<TextMeshPro>().text;
        tooltipText.transform.parent.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        tooltipText.transform.parent.gameObject.SetActive(false);
    }
}
