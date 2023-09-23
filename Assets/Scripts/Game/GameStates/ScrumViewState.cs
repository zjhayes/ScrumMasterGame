
/* Game state while viewing Scrum board. */
public class ScrumViewState : GameState
{
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;

        // Deselect current character, if any.
        controller.DeselectCharacter();

        gameManager.Camera.SwitchToBoardCamera();
        base.Handle(controller);
    }

    public override void ChangeView()
    {
        // Exit Scrum Board view.
        controller.Default();
    }

    public override void OnEscaped()
    {
        // TODO: Show settings menu.
    }

    public override void Exit()
    {
        // Escape Scrum Menu to default view when state changed.
        gameManager.UI.ScrumMenu.Escape();
        base.Exit();
    }
}
