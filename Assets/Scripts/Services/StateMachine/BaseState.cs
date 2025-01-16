
namespace HierarchicalStateMachine
{
    public abstract class BaseState<T> : IState, IStateProperties<T>, IStateEvents<T> where T : BaseState<T>
    {
        public T SuperState { get; private set; }
        public T SubState { get; private set; }

        public event StateEvent<T> OnEnter;
        public event StateEvent<T> OnUpdate;
        public event StateEvent<T> OnExit;

        public virtual void Enter()
        {
            InitializeSubState();
            SubState?.Enter();
            OnEnter?.Invoke((T)this);
        }

        public virtual void Update()
        {
            SubState?.Update();
            OnUpdate?.Invoke((T)this);
        }

        public virtual void Exit()
        {
            SubState?.Exit();
            OnExit?.Invoke((T)this);
        }

        protected abstract void InitializeSubState();

        public void SetSubState(T newSubState)
        {
            SubState = newSubState; // Change current state.
            SubState.SetSuperState((T)this);
        }

        private void SetSuperState(T newSuperState)
        {
            SuperState = newSuperState;
        }
    }
}