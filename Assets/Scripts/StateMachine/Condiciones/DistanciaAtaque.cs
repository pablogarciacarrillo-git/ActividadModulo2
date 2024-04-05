using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanciaAtaque : MyCondition
{
    public float distanciaAtaque = 0.7f;
    public GameObject character, player;

    public override bool Check()
    {
        return (Vector3.Distance(character.transform.position, player.transform.position) <= distanciaAtaque);
    }
}
