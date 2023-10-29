using HierarchicalStateMachine;

public abstract class GameState : BaseState
{
    protected IGameManager gameManager;

    protected GameState(IGameManager _gameManager) : base(_gameManager.Context as IStateMachine)
    {
        gameManager = _gameManager;
    }

    protected override void InitializeSubState() {}
}
