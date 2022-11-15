
namespace TestTask.Farm
{
    public class StateMachine
    {
        public State CurrentState { get; set; }

        public void Initialize(State _startState)
        {
            CurrentState = _startState;
            CurrentState.Enter();
        }

        public void ChangeState(State _newState)
        {
            CurrentState.Exit();
            CurrentState = _newState;
            CurrentState.Enter();
        }
    }
}
