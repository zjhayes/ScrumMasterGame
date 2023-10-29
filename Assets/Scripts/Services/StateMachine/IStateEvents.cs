namespace HierarchicalStateMachine
{
    public delegate void StateEvent<T>(T state) where T : IState;
    public interface IStateEvents<T> where T : IState
    {
        public event StateEvent<T> OnEnter;
        public event StateEvent<T> OnUpdate;
        public event StateEvent<T> OnExit;
    }
}
