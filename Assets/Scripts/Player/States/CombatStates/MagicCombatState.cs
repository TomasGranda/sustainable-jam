using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class MagicCombatState : BaseStateMachineState
    {
        private StateMachine stateMachine;

        private Stage stage;

        public MagicCombatState(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public override void ExecuteState()
        {

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var point = stage.stageCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
                if (Physics2D.Raycast(point, Vector3.forward, 100))
                {
                    stateMachine.Transition<IdleCombatState>();
                }
            }
        }

        public override void OnEnterState(params object[] objects)
        {
            Debug.Log("El Player entro en estado de magia");
            stage = (Stage)objects[0];
        }

        public override void OnExitState()
        {
            EventManager.CallEvent(EventManager.Parameter.TurnEnds);
        }
    }
}
