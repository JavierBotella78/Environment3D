using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickObject : MonoBehaviour
{
    private ObjectsManager om;
    [SerializeField]
    private GameObject ViewAssigned;
    private Material hoverMaterial;
    private Material defaultMaterial;
    private Renderer obj;
    [SerializeField]
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        ViewAssigned.SetActive(false);
        hoverMaterial = GetComponent<HoverObjects>().hoverMaterial;
        obj = transform.GetComponent<Renderer>();
        defaultMaterial = obj.material;
        om= GameObject.Find("ObjectManager").GetComponent<ObjectsManager>();
    }

    private void OnMouseUpAsButton()
    {
        if (om.openViewObject(gameObject))
        {
            ViewAssigned.SetActive(true);
            obj.material = hoverMaterial;
            GetComponent<HoverObjects>().doHover = false;
            animator.SetBool("open", true);
        }
        else
        {
            closeView();
        }
    }

    public void closeView()
    {
        ViewAssigned.SetActive(false);
        obj.material = defaultMaterial;
        GetComponent<HoverObjects>().doHover = true;
        animator.SetBool("open", false);
    }
}
