using HierarchicalStateMachine;

public abstract class GameState : BaseState<GameState>
{
    protected IGameManager gameManager;

    protected GameState(IGameManager gameManager) : base()
    {
        this.gameManager = gameManager;
    }

    protected override void InitializeSubState() {}
}
