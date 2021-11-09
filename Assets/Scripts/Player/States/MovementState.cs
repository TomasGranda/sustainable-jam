using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class MovementState : BaseStateMachineState
    {
        private Commands commands;
        private StateMachine stateMachine;
        private PlayerModel model;
        private PlayerView view;
        private GameMaster gameMaster;
        public Vector2 position;

        public MovementState(StateMachine stateMachine, PlayerModel model, PlayerView view, Rigidbody2D rigidbody2D)
        {
            commands = new Commands();
            this.gameMaster = GameMaster.Instance;

            MoveCommand moveCommand = new MoveCommand(model, view, rigidbody2D);
            JumpCommand jumpCommand = new JumpCommand(model, view, rigidbody2D, gameMaster.floorLayer);

            commands.AddKeysCommand(moveCommand, KeyCode.A, KeyCode.D);
            commands.AddKeysCommand(jumpCommand, KeyCode.Space);

            this.stateMachine = stateMachine;
            this.model = model;
            this.view = view;
        }
        
        public override void OnEnterState()
        {
            model.transform.position = position;
            
            EventManager.Subscribe(EventManager.Parameter.StartCombat, ToCombat);
        }

        public override void ExecuteState()
        {
            commands.ExecuteCommands();
        }

        private void ToCombat(params object[] _)
        {
            stateMachine.Transition<CombatState>();
        }

        public override void OnExitState()
        {
            position = model.transform.position;
        }
    }
}
