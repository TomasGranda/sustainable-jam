using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class AttackCombatState : BaseStateMachineState
    {
        private StateMachine stateMachine;
        private float time = 2;

        private Stage stage;

        public AttackCombatState(StateMachine stateMachine)
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
            Debug.Log("El Player entro en estado de ataque");
            stage = (Stage)objects[0];
        }

        public override void OnExitState()
        {
            EventManager.CallEvent(EventManager.Parameter.TurnEnds);
        }
    }
}
