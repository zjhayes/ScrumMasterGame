namespace HierarchicalStateMachine
{
    public class StateMachine
    {
        private BaseState currentState;

        public BaseState CurrentState
        {
            get { return currentState; }
            set { currentState = value; }
        }
    }
}