namespace HierarchicalStateMachine
{
    public interface IStateProperties<T> where T : IState
    {
        public StateMachine Context { get; }
        public T SuperState { get; }
        public T SubState { get; }
    }
}
