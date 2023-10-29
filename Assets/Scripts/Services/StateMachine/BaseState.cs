namespace HierarchicalStateMachine
{
    public abstract class BaseState : IState, IStateEvents<BaseState>
    {
        protected StateMachine context;
        protected BaseState currentSuperState;
        protected BaseState currentSubState;

        public event StateEvent<BaseState> OnEnter;
        public event StateEvent<BaseState> OnUpdate;
        public event StateEvent<BaseState> OnExit;

        public BaseState(StateMachine _context)
        {
            context = _context;
        }

        public virtual void Enter()
        {
            currentSubState?.Enter();
            OnEnter?.Invoke(this);
        }

        public virtual void Update()
        {
            currentSubState?.Update();
            OnUpdate?.Invoke(this);
        }

        public virtual void Exit()
        {
            currentSubState?.Exit();
            OnExit?.Invoke(this);
        }

        public void SwitchState(BaseState newState)
        {
            Exit(); // Exit current state.
            newState.Enter();
            if(currentSuperState == null)
            {
                context.CurrentState = newState;
            }
            else
            {
                currentSuperState.SetSubState(newState);
            }
        }

        protected void SetSubState(BaseState newSubState) 
        {
            currentSubState = newSubState; // Change current state.
            currentSubState.SetSuperState(this);
        }

        // Adds sub-state to end of heirarchy.
        protected void AddSubState(BaseState newSubState)
        {
            if (currentSubState == null)
            {
                SetSubState(newSubState);
            }
            else
            {
                currentSubState.AddSubState(newSubState);
            }
        }

        private void SetSuperState(BaseState newSuperState)
        {
            currentSuperState = newSuperState;
        }
    }
}