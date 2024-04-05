using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muerto : MyCondition
{
    public Enemy enemy;
    public GameObject character;

    public override bool Check()
    {
        if (enemy == null)
        {
            enemy = character.GetComponent<Enemy>();
        }

        return (enemy.currentHealth <= 0);
    }
}
