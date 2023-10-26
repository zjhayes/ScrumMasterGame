namespace StateMachine
{
    public abstract class BaseState
    {
        protected StateMachine context;
        protected StateFactory factory;
        protected BaseState currentSuperState;
        protected BaseState currentSubState;
        protected bool isRoot = false;

        public BaseState(StateMachine _context, StateFactory _factory)
        {
            context = _context;
            factory = _factory;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
        public abstract void CheckSwitchStates();
        public abstract void InitializeSubState();
        public void UpdateStates() 
        {
            Update();
            currentSubState?.Update();
            CheckSwitchStates();
        }
        private void SwitchState(BaseState newState)
        {
            Exit(); // Exit current state.
            newState.Enter();
            if(isRoot)
            {
                context.CurrentState = newState;
            }
            else if(currentSuperState != null)
            {
                currentSuperState.SetSubState(newState);
            }
        }

        protected void SetSuperState(BaseState newSuperState) 
        {
            currentSuperState = newSuperState;
        }

        protected void SetSubState(BaseState newSubState) 
        {
            currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }

    }

}