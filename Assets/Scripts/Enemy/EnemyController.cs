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

        IdleCombatState idleCombatState = new IdleCombatState(stateMachine, GetComponent<Rigidbody2D>());
        TurnCombatState turnCombatState = new TurnCombatState(stateMachine);

        MovementState movementState = new MovementState(stateMachine, GetComponent<Rigidbody2D>());

        idleCombatState.AddTransition(turnCombatState);
        turnCombatState.AddTransition(idleCombatState);

        movementState.AddTransition(idleCombatState);
        idleCombatState.AddTransition(movementState);

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
