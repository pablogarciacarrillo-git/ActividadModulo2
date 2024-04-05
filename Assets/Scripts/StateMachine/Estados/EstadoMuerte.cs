using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoMuerte : MyState
{
    public Animator animator;

    public GameObject character;

    public override void DoAction()
    {
        if (animator == null)
        {
            animator = character.GetComponent<Animator>();
        }
        
        animator.SetBool("Attack", false);
        animator.SetBool("Alive", false);

        character.transform.localScale = character.transform.localScale * 0.995f;
    }
}
