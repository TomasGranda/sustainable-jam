using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class MovementState : BaseStateMachineState
    {
        private StateMachine stateMachine;
        private Rigidbody2D rigidbody;

        public MovementState(StateMachine stateMachine, Rigidbody2D rigidbody)
        {
            this.stateMachine = stateMachine;
            this.rigidbody = rigidbody;
        }

        public override void ExecuteState()
        {

        }

        private void ToCombat(params object[] _)
        {
            stateMachine.Transition<IdleCombatState>();
        }

        public override void OnEnterState(params object[] objects)
        {
            EventManager.Subscribe(EventManager.Parameter.StartCombat, ToCombat);
        }

        public override void OnExitState()
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
}

