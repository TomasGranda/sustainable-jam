using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnemyStates;

public class EnemyController : MonoBehaviour
{
    private StateMachine stateMachine;

    private EnemyModel model;

    private EnemyView view;

    void Start()
    {
        model = GetComponent<EnemyModel>();
        view = GetComponent<EnemyView>();

        stateMachine = new StateMachine();

        IdleState idleState = new IdleState();
        TurnState turnState = new TurnState();

        MovementState movementState = new MovementState(stateMachine, GetComponent<Rigidbody2D>());

        idleState.AddTransition(turnState);
        turnState.AddTransition(idleState);

        movementState.AddTransition(idleState);
        idleState.AddTransition(movementState);

        stateMachine.Init(movementState);
    }

    void Update()
    {
        stateMachine.OnUpdate();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var playerModel = other.collider.gameObject.GetComponent<PlayerModel>();
        if (playerModel != null)
        {
            EventManager.CallEvent(EventManager.Parameter.StartCombat, model.combatStage, model.team, playerModel.team);
        }
    }
}
