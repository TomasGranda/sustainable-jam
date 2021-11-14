using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerStates;

public class PlayerController : MonoBehaviour
{
    private PlayerModel model;
    private PlayerView view;

    private StateMachine stateMachine;

    void Start()
    {
        view = GetComponent<PlayerView>();
        model = GetComponent<PlayerModel>();

        stateMachine = new StateMachine();

        MovementState movementState = new MovementState(stateMachine, model, view, GetComponent<Rigidbody2D>());
        IdleCombatState idleCombatState = new IdleCombatState(stateMachine, GetComponent<Rigidbody2D>(), view, model);
        TurnCombatState turnCombatState = new TurnCombatState(stateMachine);
        AttackCombatState attackCombatState = new AttackCombatState(stateMachine, model, view);
        MagicCombatState magicCombatState = new MagicCombatState(stateMachine);

        movementState.AddTransition(idleCombatState);
        idleCombatState.AddTransition(movementState);

        idleCombatState.AddTransition(turnCombatState);

        turnCombatState.AddTransition(idleCombatState);
        turnCombatState.AddTransition(attackCombatState);
        turnCombatState.AddTransition(magicCombatState);
        attackCombatState.AddTransition(idleCombatState);
        magicCombatState.AddTransition(idleCombatState);

        stateMachine.Init(movementState);
    }

    void Update()
    {
        stateMachine.OnUpdate();
    }
}
