using HierarchicalStateMachine;

public class RetrospectiveState : PausableState
{

    public RetrospectiveState(IGameManager _gameManager) : base(_gameManager) {}

    public override void Enter()
    {
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
        SetSubState(gameManager.Context.GetState(GameStates.BOARD_VIEW));
    }
}
