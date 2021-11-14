using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerStates
{
    public class AttackCombatState : BaseStateMachineState
    {
        private StateMachine stateMachine;
        private PlayerModel model;
        private PlayerView view;
        private Stage stage;

        public AttackCombatState(StateMachine stateMachine, PlayerModel model, PlayerView view)
        {
            this.stateMachine = stateMachine;
            this.model = model;
            this.view = view;
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
            view.animator.SetTrigger("Attack");
            EventManager.CallEvent(EventManager.Parameter.TurnEnds);
        }
    }
}
