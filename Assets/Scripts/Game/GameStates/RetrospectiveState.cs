
public class RetrospectiveState : PausableState
{

    public RetrospectiveState(IGameManager _gameManager) : base(_gameManager) {}

    public override void Enter()
    {
        gameManager.Team.RallyTeam();
        gameManager.UI.ScrumMenu.Hide();
        gameManager.UI.RetrospectiveMenu.Show();
        
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        // Show scrum board when state changed.
        gameManager.UI.RetrospectiveMenu.Hide();
        gameManager.UI.ScrumMenu.Show();
    }

    protected override void InitializeSubState()
    {
        SetSubState(gameManager.Context.StateMachine.GetState(GameStates.BOARD_VIEW));
    }
}
