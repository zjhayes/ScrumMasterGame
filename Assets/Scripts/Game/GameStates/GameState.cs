using HierarchicalStateMachine;

public abstract class GameState : BaseState
{
    protected IGameManager gameManager;

    protected GameState(IGameManager _gameManager, StateMachine _context) : base(_context)
    {
        gameManager = _gameManager;
    }
}
