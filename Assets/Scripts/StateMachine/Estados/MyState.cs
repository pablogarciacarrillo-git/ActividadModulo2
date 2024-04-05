using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyState
{
    public List<MyTransition> transitions = new List<MyTransition>();

    public abstract void DoAction();

    public void AddTransition(MyTransition transition)
    {
        transitions.Add(transition);
    }
}
