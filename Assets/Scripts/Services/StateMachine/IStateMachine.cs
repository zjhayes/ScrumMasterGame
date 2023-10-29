namespace HierarchicalStateMachine
{
    public interface IStateMachine
    {
        public BaseState CurrentState { get; set; }
    }
}