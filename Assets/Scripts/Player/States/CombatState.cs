using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class CombatState : BaseStateMachineState
    {
        private StateMachine stateMachine;
        private Rigidbody2D rigidbody;

        public CombatState(StateMachine stateMachine, Rigidbody2D rigidbody)
        {
            this.stateMachine = stateMachine;
            this.rigidbody = rigidbody;
        }

        public override void ExecuteState()
        {
        }

        public override void OnEnterState()
        {
            rigidbody.velocity = Vector2.zero;
        }

        public override void OnExitState()
        {

        }
    }
}
