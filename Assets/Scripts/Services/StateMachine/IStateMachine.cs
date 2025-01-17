namespace HierarchicalStateMachine
{
    public interface IStateMachine<T> where T : IState
    {
        public T CurrentState { get; set; }
    }
}