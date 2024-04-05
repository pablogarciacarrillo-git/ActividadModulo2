using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EstadoPersecucion : MyState
{
    public NavMeshAgent agent;
    public GameObject character, player;

    public Animator animator;

    public override void DoAction()
    {
        if (agent == null)
        {
            agent = character.GetComponent<NavMeshAgent>();
        }

        if (animator == null)
        {
            animator = character.GetComponent<Animator>();
        }
        
        animator.SetBool("Attack", false);

        agent.SetDestination(player.transform.position);
    }
}
