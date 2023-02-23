
public interface IGameState : IState<IContextManager>
{
    public abstract void Escape();
    public abstract void ChangeView();
}
