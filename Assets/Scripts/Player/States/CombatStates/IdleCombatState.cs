using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class IdleCombatState : BaseStateMachineState
    {
        private StateMachine stateMachine;
        private Rigidbody2D rigidbody;
        private PlayerView view;
        private PlayerModel model;

        public IdleCombatState(StateMachine stateMachine, Rigidbody2D rigidbody, PlayerView view, PlayerModel model)
        {

            this.stateMachine = stateMachine;
            this.rigidbody = rigidbody;
            this.view = view;
            this.model = model;
        }

        public override void ExecuteState()
        {
        }

        public override void OnEnterState(params object[] objects)
        {
            if (model.life <= 0)
            {
                view.animator.SetTrigger("Death");
            }
            model.sprite.flipX = false;
            view.animator.SetFloat("Speed", 0);
            view.animator.SetBool("Jump", false);
            rigidbody.velocity = Vector2.zero;
            EventManager.Subscribe(EventManager.Parameter.TurnStarts, OnTurnStars);
            EventManager.Subscribe(EventManager.Parameter.EndCombat, EndCombat);
        }

        private void EndCombat(params object[] objects)
        {
            model.EndCombat(stateMachine);
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
