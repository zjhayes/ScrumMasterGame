
public class ScrumState : GameState
{
    public ScrumState(IGameManager _gameManager) : base(_gameManager) {}

    public override void Enter()
    {
        gameManager.Team.Scrum();
        base.Enter();
    }

    protected override void InitializeSubState()
    {
        SetSubState(gameManager.Context.StateMachine.GetState(GameStates.TOGGLE_VIEW));
    }
}
