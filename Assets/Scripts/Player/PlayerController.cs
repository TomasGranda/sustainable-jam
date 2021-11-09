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
        CombatState combatState = new CombatState(stateMachine, GetComponent<Rigidbody2D>());

        movementState.AddTransition(combatState);
        combatState.AddTransition(movementState);

        stateMachine.Init(movementState);
    }

    void Update()
    {
        stateMachine.OnUpdate();
    }
}
