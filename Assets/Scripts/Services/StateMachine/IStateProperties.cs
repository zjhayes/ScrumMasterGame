namespace HierarchicalStateMachine
{
    public interface IStateProperties<T> where T : IState
    {
        public T SuperState { get; }
        public T SubState { get; }
    }
}
