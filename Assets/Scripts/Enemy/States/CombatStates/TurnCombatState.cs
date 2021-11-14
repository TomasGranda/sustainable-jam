using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class TurnCombatState : BaseStateMachineState
    {
        private StateMachine stateMachine;
        private EnemyView view;
        private float time = 2;
        private float time2 = 0.5f;

        public TurnCombatState(StateMachine stateMachine, EnemyView view)
        {
            this.stateMachine = stateMachine;
            this.view = view;
        }

        public override void ExecuteState()
        {
            time2 -= Time.deltaTime;
            if (time2 <= 0)
            {
                time2 = 0.5f;
                view.animator.SetBool("Attack", true);
            }

            time -= Time.deltaTime;
            if (time <= 0)
            {
                view.animator.SetBool("Attack", false);
                time = 1.5f;
                stateMachine.Transition<IdleCombatState>();
            }
        }

        public override void OnEnterState(params object[] objects)
        {
            Debug.Log("El Enemy entro en estado de turno");
        }

        public override void OnExitState()
        {
            EventManager.CallEvent(EventManager.Parameter.TurnEnds);
        }
    }
}
