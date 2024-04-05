using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;

public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    //public Transform target;

    public float maxHealth = 100;
    public float currentHealth;
    //public bool isAlive = true;

    protected Animator animator;

    // Start is called before the first frame update
    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        //isAlive = true;
        animator.SetBool("Alive", true);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        animator.SetBool("Moving", agent.velocity != Vector3.zero);

        /*
        if (!isAlive)
        {
            animator.SetBool("Alive", false);
        } else
        {
            animator.SetBool("Moving", agent.velocity != Vector3.zero);
        }
        */
        /*
        float distance = Vector3.Distance(agent.transform.position, target.transform.position);
        if (distance < 10)
        {
            agent.SetDestination(target.position);
        }
        */
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    void LateUpdate()
    {
        animator.SetBool("Damage", false);
    }

    public void ReceiveDamage(float damage)
    {
        /*
        if (!isAlive)
        {
            return;
        }
        */
        
        Debug.Log("OW (" + damage + " damage)");
        currentHealth -= damage;
        animator.SetBool("Damage", true);

        /*
        if (currentHealth <= 0)
        {
            //isAlive = false;
        } else
        {
            animator.SetBool("Damage", true);
        }
        */

        //Aquí se puede añadir un tiempo de invencibilidad desactivando el collider
    }

    public void StopMoving()
    {
        agent.isStopped = true;
    }

    public void StartMoving()
    {
        agent.isStopped = false;
    }

    public virtual void DestroyEnemy()
    {
        Destroy(gameObject);
    }

}
