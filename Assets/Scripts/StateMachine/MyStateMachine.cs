using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MyStateMachine : MonoBehaviour
{
    public MyState estadoActual;
    public string nombreEstadoActual;

    // Update is called once per frame
    void Update()
    {
        nombreEstadoActual = estadoActual.GetType().Name;

        foreach (MyTransition transition in estadoActual.transitions)
        {
            if (transition.CheckCondition())
            {
                estadoActual = transition.destinationState;
            }
        }

        estadoActual.DoAction();
        
    }
}
