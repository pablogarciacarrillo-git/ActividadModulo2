using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class SlimeStateMachine : Enemy
{
    public MyStateMachine stateMachine;

    public GameObject player;

    public List<Transform> puntosPatrulla = new List<Transform>();

    public BoxCollider colliderAtaque;

    public float damage = 10;
    public int points = 20;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        colliderAtaque = GetComponent<BoxCollider>();
        colliderAtaque.enabled = false;

        stateMachine = GetComponent<MyStateMachine>();

        // Creación de estados
        EstadoPatrulla patrulla = new EstadoPatrulla();
        stateMachine.estadoActual = patrulla;

        patrulla.puntosPatrulla = puntosPatrulla;
        patrulla.character = gameObject;

        EstadoPersecucion persecucion = new EstadoPersecucion();
        
        persecucion.character = gameObject;
        persecucion.player = player;

        EstadoAtaque ataque = new EstadoAtaque();
        
        ataque.character = gameObject;
        ataque.player = player;

        EstadoMuerte muerte = new EstadoMuerte();
        
        muerte.character = gameObject;

        // Creación de condiciones
        CercaJugador cerca = new CercaJugador();
        cerca.character = gameObject;
        cerca.player = player;

        CercaJugador lejos = new CercaJugador();
        lejos.character = gameObject;
        lejos.player = player;
        lejos.inverted = true;

        DistanciaAtaque cercaAtaque = new DistanciaAtaque();
        cercaAtaque.character = gameObject;
        cercaAtaque.player = player;

        DistanciaAtaque lejosAtaque = new DistanciaAtaque();
        lejosAtaque.character = gameObject;
        lejosAtaque.player = player;
        lejosAtaque.inverted = true;

        Muerto muerto = new Muerto();
        muerto.character = gameObject;

        // Creación de transiciones
        MyTransition patrullaAPersecucion = new MyTransition();
        patrullaAPersecucion.condition = cerca;
        patrullaAPersecucion.destinationState = persecucion;

        MyTransition persecucionAPatrulla = new MyTransition();
        persecucionAPatrulla.condition = lejos;
        persecucionAPatrulla.destinationState = patrulla;

        MyTransition persecucionAAtaque = new MyTransition();
        persecucionAAtaque.condition = cercaAtaque;
        persecucionAAtaque.destinationState = ataque;

        MyTransition ataqueAPersecucion = new MyTransition();
        ataqueAPersecucion.condition = lejosAtaque;
        ataqueAPersecucion.destinationState = persecucion;

        MyTransition todoAMuerte = new MyTransition();
        todoAMuerte.condition = muerto;
        todoAMuerte.destinationState = muerte;

        // Adición de transiciones a estados
        patrulla.AddTransition(patrullaAPersecucion);
        patrulla.AddTransition(todoAMuerte);

        persecucion.AddTransition(persecucionAPatrulla);
        persecucion.AddTransition(persecucionAAtaque);
        persecucion.AddTransition(todoAMuerte);

        ataque.AddTransition(ataqueAPersecucion);
        ataque.AddTransition(todoAMuerte);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public void ActivateAttack()
    {
        colliderAtaque.enabled = true;
    }

    public void DeactivateAttack()
    {
        colliderAtaque.enabled = false;
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        PlayerController controller = other.gameObject.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.receiveDamage(damage);
            //Debug.Log("Attacked Player");
        }
        
    }

    public override void DestroyEnemy()
    {
        PlayerController controller = player.GetComponent<PlayerController>();

        if (controller != null)
        {
            controller.score += points;
        }

        base.DestroyEnemy();
    }
}
