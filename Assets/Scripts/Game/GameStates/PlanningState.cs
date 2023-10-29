
public class PlanningState : PausableState
{
    public PlanningState(IGameManager _gameManager) : base(_gameManager) {}

    public override void Enter()
    {
        gameManager.UI.PlanningMenu.Show();
        gameManager.UI.ScrumMenu.Hide();
        gameManager.UI.StatusBar.Hide();
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        // Show scrum board when state changed.
        gameManager.UI.PlanningMenu.Hide();
        gameManager.UI.ScrumMenu.Show();
        gameManager.UI.StatusBar.Show();
    }

    protected override void InitializeSubState()
    {
        SetSubState(gameManager.Context.GetState(GameStates.BOARD_VIEW));
    }
}
