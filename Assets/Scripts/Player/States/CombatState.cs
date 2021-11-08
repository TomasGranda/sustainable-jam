using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class CombatState : BaseStateMachineState
    {
        private StateMachine stateMachine;

        public CombatState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void ExecuteState()
        {
        }

        public override void OnEnterState()
        {
            // TODO: Revisar quitar fisicas al cambiar de estado
        }

        public override void OnExitState()
        {

        }
    }
}
