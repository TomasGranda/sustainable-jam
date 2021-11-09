using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class IdleCombatState : BaseStateMachineState
    {
        private StateMachine stateMachine;
        private Rigidbody2D rigidbody;

        public IdleCombatState(StateMachine stateMachine, Rigidbody2D rigidbody)
        {
            this.stateMachine = stateMachine;
            this.rigidbody = rigidbody;
        }

        public override void ExecuteState()
        {
        }

        public override void OnEnterState(params object[] objects)
        {
            rigidbody.velocity = Vector2.zero;
            EventManager.Subscribe(EventManager.Parameter.TurnStarts, OnTurnStars);
        }

        private void OnTurnStars(params object[] objects)
        {
            var fighter = (BaseFighter)objects[0];
            if (fighter.name == rigidbody.name)
            {
                stateMachine.Transition<TurnCombatState>();
            }
        }

        public override void OnExitState()
        {
            EventManager.Unsubscribe(EventManager.Parameter.TurnStarts, OnTurnStars);
        }
    }
}
