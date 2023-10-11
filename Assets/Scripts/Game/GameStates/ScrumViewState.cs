
/* Game state while viewing Scrum board. */
public class ScrumViewState : GameState
{
    private ContextManager gameContext;

    public override void Handle(ContextManager _controller)
    {
        gameContext = _controller;

        // Deselect current character, if any.
        gameContext.DeselectCharacter();
        gameManager.Camera.SwitchToBoardCamera();
        base.Handle(gameContext);
    }

    public override void ChangeView()
    {
        // Exit Scrum Board view.
        gameContext.Default();
    }

    public override void OnEscaped()
    {
        gameContext.Pause();
    }

    public override void Exit()
    {
        // Escape Scrum Menu to default view when state changed.
        gameManager.UI.ScrumMenu.Escape();
        base.Exit();
    }
}
