using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MyCondition
{
    public bool inverted = false;
    public abstract bool Check();

    public bool CheckInverted()
    {
        if (inverted)
        {
            return !(Check());
        } else
        {
            return Check();
        }
    }
}
