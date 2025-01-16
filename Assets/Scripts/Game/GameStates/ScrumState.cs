

public class ScrumState : PausableState
{
    public ScrumState(IGameManager _gameManager) : base(_gameManager) {}

    protected override void InitializeSubState()
    {
        SetSubState(gameManager.Context.StateMachine.GetState(GameStates.TOGGLE_VIEW));
    }
}
