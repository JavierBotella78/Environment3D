using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEvent : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("IsOpen", true);
        animator.SetBool("IsClose", false);
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("IsOpen", false);
            animator.SetBool("IsClose", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("IsOpen", true);
            animator.SetBool("IsClose", false);

        }



    }
}
