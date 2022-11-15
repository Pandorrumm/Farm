using UnityEngine;

namespace TestTask.Farm
{
    public class PlantState : State
    {
        private Animator animator;
        private const string ANIMATION_KEY = "Plant";

        public PlantState(Animator _animator)
        {
            animator = _animator;
        }

        public override void Enter()
        {
            base.Enter();
            animator.CrossFade(ANIMATION_KEY, 0.1f);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
