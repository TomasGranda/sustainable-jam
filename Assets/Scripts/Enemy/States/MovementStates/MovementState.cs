using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace EnemyStates
{
    public class MovementState : BaseStateMachineState
    {
        private StateMachine stateMachine;
        private Rigidbody2D rigidbody;
        private EnemyModel model;
        private int pivot = 0;

        public MovementState(StateMachine stateMachine, Rigidbody2D rigidbody, EnemyModel model)
        {
            this.stateMachine = stateMachine;
            this.rigidbody = rigidbody;
            this.model = model;
        }

        public override void ExecuteState()
        {
            var pivotPosition = model.points[pivot].position - model.transform.position;
            if (Mathf.Abs(pivotPosition.x) <= 0.2f)
            {
                pivot++;
                if (pivot == 2)
                {
                    pivot = 0;
                }
            }
            Vector2 movement = (model.transform.right * pivotPosition.x).normalized * model.speed;

            rigidbody.velocity = new Vector2(movement.x, rigidbody.velocity.y);

            if (movement.x > 0)
            {
                model.sprite.flipX = true;
            }
            else
            {
                model.sprite.flipX = false;
            }
        }

        private void ToCombat(params object[] objects)
        {
            var fighter = ((List<GameObject>)objects[1]).First().GetComponent<BaseFighter>();
            if (fighter.gameObject.name == rigidbody.name)
            {
                stateMachine.Transition<IdleCombatState>();
            }
        }

        public override void OnEnterState(params object[] objects)
        {
            EventManager.Subscribe(EventManager.Parameter.StartCombat, ToCombat);
        }

        public override void OnExitState()
        {
            rigidbody.velocity = Vector2.zero;
        }
    }
}

