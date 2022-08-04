using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickButtMedia : MonoBehaviour
{
    private bool openButt = false;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseUpAsButton()
    {
        if (!openButt)
        {
            animator.SetInteger("open",1);
            openButt = true;
        }
        else
        {
            animator.SetInteger("open",0);
            openButt = false;
        }

    }
}
