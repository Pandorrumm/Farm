using UnityEngine;
using UnityEngine.AI;
using System;
using DG.Tweening;

namespace TestTask.Farm
{
    public class WalkingState : State
    {
        private Farmer farmer;
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private Vector3 targetPosition;
        private int indexEarthCell;
        private bool isHarvest;
        private float distance;
        private const string ANIMATION_KEY = "Walking";

        public static Action<int, bool> OnCameToTarget;

        public WalkingState(Farmer _farmer, Animator _animator, NavMeshAgent _agent, Vector3 _targetPosition, int _indexEarthCell, bool _isHarvest)
        {
            farmer = _farmer;
            navMeshAgent = _agent;
            animator = _animator;
            targetPosition = _targetPosition;
            indexEarthCell = _indexEarthCell;
            isHarvest = _isHarvest;
        }

        public override void Enter()
        {
            base.Enter();

            animator.CrossFade(ANIMATION_KEY, 0.1f);
            RotateTowardsTarget();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            navMeshAgent.SetDestination(targetPosition);
            distance = Vector3.Distance(targetPosition, farmer.transform.position);

            if (distance < navMeshAgent.stoppingDistance)
            {
                RotateTowardsTarget();
                navMeshAgent.SetDestination(navMeshAgent.transform.position);
                OnCameToTarget?.Invoke(indexEarthCell, isHarvest);
            }
        }

        private void RotateTowardsTarget()
        {
            Quaternion lookRotation = Quaternion.LookRotation(targetPosition - farmer.transform.position);
            farmer.transform.DORotateQuaternion(lookRotation, 0.5f);
        }
    }
}
