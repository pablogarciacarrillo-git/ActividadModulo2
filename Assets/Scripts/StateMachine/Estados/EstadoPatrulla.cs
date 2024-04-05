using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EstadoPatrulla : MyState
{
    public List<Transform> puntosPatrulla = new List<Transform>();
    public int indiceSiguientePatrulla = 0;

    public float minDistance = 1;

    public NavMeshAgent agent;
    public GameObject character;
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
        
        if (Vector3.Distance(character.transform.position, puntosPatrulla[indiceSiguientePatrulla].transform.position) <= minDistance)
        {
            indiceSiguientePatrulla++;
            if (indiceSiguientePatrulla >= puntosPatrulla.Count)
            {
                indiceSiguientePatrulla = 0;
            }
        }

        animator.SetBool("Attack", false);
        agent.SetDestination(puntosPatrulla[indiceSiguientePatrulla].position);

    }
}
