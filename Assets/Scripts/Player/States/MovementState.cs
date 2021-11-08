using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementState : BaseStateMachineState
{
    private Commands commands;

    private StateMachine stateMachine;
    private PlayerModel model;
    private PlayerView view;

    private GameMaster gameMaster;

    public MovementState(StateMachine stateMachine, PlayerModel model, PlayerView view, Rigidbody2D rigidbody2D)
    {
        commands = new Commands();
        this.gameMaster = GameMaster.GetGameMaster();

        MoveCommand moveCommand = new MoveCommand(model, view, rigidbody2D);
        JumpCommand jumpCommand = new JumpCommand(model, view, rigidbody2D, gameMaster.floorLayer);

        commands.AddKeysCommand(moveCommand, KeyCode.A, KeyCode.D);
        commands.AddKeysCommand(jumpCommand, KeyCode.Space);

        this.stateMachine = stateMachine;
        this.model = model;
        this.view = view;
    }

    public override void ExecuteState()
    {
        commands.ExecuteCommands();
        ToCombat();
    }

    private void ToCombat()
    {
        if (Physics2D.OverlapCircle(model.transform.position, model.combatTriggerRange, gameMaster.enemyLayer))
        {
            stateMachine.Transition<CombatState>();
        }
    }

    public override void OnEnterState() { }

    public override void OnExitState() { }
}
