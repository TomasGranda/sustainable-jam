using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyStates
{
    public class TurnCombatState : BaseStateMachineState
    {
        private StateMachine stateMachine;

        private float time = 2;

        public TurnCombatState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void ExecuteState()
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 2;
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
