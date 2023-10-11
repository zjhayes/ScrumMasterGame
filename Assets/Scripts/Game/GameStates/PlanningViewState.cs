
public class PlanningViewState : GameState
{
    private ContextManager gameContext;

    public override void Handle(ContextManager _controller)
    {
        gameContext = _controller;

        UpdateView();
        base.Handle(gameContext);
    }

    public override void OnEscaped()
    {
        gameContext.Pause();
    }

    public override void Exit()
    {
        // Show scrum board when state changed.
        gameManager.UI.PlanningMenu.Hide();
        gameManager.UI.ScrumMenu.Show();
        gameManager.UI.StatusBar.Show();
        base.Exit();
    }

    private void UpdateView()
    {
        gameManager.UI.ScrumMenu.Hide();
        gameManager.UI.StatusBar.Hide();
        gameManager.UI.PlanningMenu.Show();
        gameManager.Camera.SwitchToBoardCamera();
    }
}
