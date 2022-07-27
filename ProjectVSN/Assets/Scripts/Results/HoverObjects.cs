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

    void Start()
    {
        if(obj==null)
            obj = transform.GetComponent<Renderer>();
        defaultMaterial = obj.material;
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
        obj.material = defaultMaterial;
        hand.SetBool("IsHover", false);

    }

    private void OnMouseUpAsButton()
    {
        obj.material = defaultMaterial;
        hand.SetBool("IsHover", false);

    }

}
