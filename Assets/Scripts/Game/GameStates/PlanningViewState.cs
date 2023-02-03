using UnityEngine;

public class PlanningViewState : GameState
{
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;

        gameManager.UI.ScrumMenu.Hide();
        gameManager.UI.PlanningMenu.Show();
        gameManager.Camera.SwitchToBoardCamera();
        base.Handle(controller);
    }

    public override void Escape()
    {
        // TODO: Show settings menu.
    }

    public override void Destroy()
    {
        // Show scrum board when state changed.
        gameManager.UI.PlanningMenu.Hide();
        gameManager.UI.ScrumMenu.Show();
        base.Destroy();
    }
}
