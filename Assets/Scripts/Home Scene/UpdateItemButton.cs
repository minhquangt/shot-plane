using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
public class UpdateItemButton : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Change()
    {
        animator.SetBool("change", true);
    }

    public void Idle()
    {
        animator.SetBool("change", false);
    }


}
