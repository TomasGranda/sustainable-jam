using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class TurnCombatState : BaseStateMachineState
    {
        private StateMachine stateMachine;

        public TurnCombatState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void ExecuteState() { }

        private void OnActionSelected(params object[] objects)
        {
            switch ((CombatManager.Parameter)objects[0])
            {
                case CombatManager.Parameter.Attack:
                    stateMachine.Transition<AttackCombatState>((Stage)objects[1]);
                    return;
                case CombatManager.Parameter.Magic:
                    stateMachine.Transition<MagicCombatState>((Stage)objects[1]);
                    return;
                case CombatManager.Parameter.Skip:

                    EventManager.CallEvent(EventManager.Parameter.TurnEnds);
                    stateMachine.Transition<IdleCombatState>();
                    return;
                default:
                    return;
            }
        }

        public override void OnEnterState(params object[] objects)
        {
            EventManager.Subscribe(EventManager.Parameter.Action, OnActionSelected);
        }

        public override void OnExitState()
        {

        }
    }
}
