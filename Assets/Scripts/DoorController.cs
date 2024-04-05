using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    public Boolean opened = false;
    public Animator animator;
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (opened == true)
        {
            animator.SetBool("Open", true);
        }
        else {
            animator.SetBool("Open", false);
        }    
    }
}
