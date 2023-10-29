namespace HierarchicalStateMachine
{
    public abstract class BaseState : IState, IStateProperties<BaseState>, IStateEvents<BaseState>
    {
        public IStateMachine Context { get; private set; }
        public BaseState SuperState { get; private set; }
        public BaseState SubState { get; private set; }

        public event StateEvent<BaseState> OnEnter;
        public event StateEvent<BaseState> OnUpdate;
        public event StateEvent<BaseState> OnExit;

        public BaseState(IStateMachine _context)
        {
            Context = _context;
        }

        public virtual void Enter()
        {
            InitializeSubState();
            SubState?.Enter();
            OnEnter?.Invoke(this);
        }

        public virtual void Update()
        {
            SubState?.Update();
            OnUpdate?.Invoke(this);
        }

        public virtual void Exit()
        {
            SubState?.Exit();
            OnExit?.Invoke(this);
        }

        public void SwitchState(BaseState newState)
        {
            Exit(); // Exit current state.
            if(SuperState == null)
            {
                Context.CurrentState = newState;
            }
            else
            {
                SuperState.SetSubState(newState);
            }
            newState.Enter();
        }

        protected abstract void InitializeSubState();

        public void SetSubState(BaseState newSubState) 
        {
            SubState = newSubState; // Change current state.
            SubState.SetSuperState(this);
        }

        private void SetSuperState(BaseState newSuperState)
        {
            SuperState = newSuperState;
        }
    }
}