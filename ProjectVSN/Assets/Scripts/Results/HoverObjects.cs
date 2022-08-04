using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverObjects : MonoBehaviour
{
    public Material hoverMaterial;
    private Material defaultMaterial;
    [SerializeField]
    private Renderer obj;
    private Animator hand;
    public bool doHover = true;

    void Start()
    {
        if(obj==null)
            obj = transform.GetComponent<Renderer>();
        defaultMaterial = obj.material;
        if (hoverMaterial == null)
            hoverMaterial = defaultMaterial;
        hand = GameObject.FindGameObjectWithTag("Hand").GetComponent<Animator>();
        hand.SetBool("IsHover", false);

    }

    private void OnMouseOver()
    {
        obj.material = hoverMaterial;
        hand.SetBool("IsHover", true);
    }

    private void OnMouseExit()
    {
        if(doHover)
            obj.material = defaultMaterial;
        hand.SetBool("IsHover", false);

    }

    private void OnMouseUpAsButton()
    {
        if (doHover)
            obj.material = defaultMaterial;
        hand.SetBool("IsHover", false);

    }

}
