using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CercaJugador : MyCondition
{
    public float distanciaPersecucion = 7;
    public GameObject character, player;

    public override bool Check()
    {
        return (Vector3.Distance(character.transform.position, player.transform.position) <= distanciaPersecucion);
    }
}
