using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class IdleCombatState : BaseStateMachineState
    {
        private StateMachine stateMachine;
        private Rigidbody2D rigidbody;
        private EnemyModel model;
        private EnemyView view;

        public IdleCombatState(StateMachine stateMachine, Rigidbody2D rigidbody, EnemyModel model, EnemyView view)
        {
            this.stateMachine = stateMachine;
            this.rigidbody = rigidbody;
            this.model = model;
            this.view = view;
        }

        public override void ExecuteState()
        {
        }

        public override void OnEnterState(params object[] objects)
        {
            model.sprite.flipX = false;
            rigidbody.velocity = Vector2.zero;
            EventManager.Subscribe(EventManager.Parameter.TurnStarts, OnTurnStars);
        }

        private void OnTurnStars(params object[] objects)
        {
            var fighter = (BaseFighter)objects[0];
            if (fighter.gameObject.name == rigidbody.name)
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
