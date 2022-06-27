﻿using UnityEngine;
using UnityEngine.AI;

namespace Tanks.Game.AI
{
    public class EnemyAnimator : MonoBehaviour
    {
        public enum MovementAxis
        {
            XAxis,
            ZAxis
        }

        [SerializeField]
        private Animator animator;
        [SerializeField]
        private NavMeshAgent navMeshAgent;

        private void Update()
        {
            var localSpeed = transform.InverseTransformVector(navMeshAgent.velocity);
            SetSpeed(MovementAxis.ZAxis, localSpeed.z);
        }

        public void OnAttack()
        {
            animator.SetTrigger("attack");
        }

        public void OnDeath()
        {
            animator.SetTrigger("death");
        }

        public void SetSpeed(MovementAxis axis, float value)
        {
            switch (axis)
            {
                case MovementAxis.XAxis:
                    animator.SetFloat("x_speed", value);
                    break;
                case MovementAxis.ZAxis:
                    animator.SetFloat("z_speed", value);
                    break;
            }
        }
    }
}
