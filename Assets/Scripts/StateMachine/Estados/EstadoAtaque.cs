using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoAtaque : MyState
{
    public Animator animator;

    public GameObject character, player;

    public override void DoAction()
    {
        if (animator == null)
        {
            animator = character.GetComponent<Animator>();
        }

        //character.transform.LookAt(player.transform.position);
        animator.SetBool("Attack", true);
    }
}
