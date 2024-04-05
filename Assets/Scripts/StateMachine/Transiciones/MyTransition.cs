using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTransition
{
    public MyCondition condition;

    public MyState destinationState;

    public bool CheckCondition()
    {
        return condition.CheckInverted();
    }
}
