using UnityEngine;

public class PlanningViewState : GameState
{
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;

        controller.GameManager.UI.ScrumMenu.Hide();
        controller.GameManager.UI.PlanningMenu.Show();
        controller.GameManager.Camera.SwitchToBoardCamera();
    }

    public override void Escape()
    {
        // TODO: Show settings menu.
    }

    public override void Destroy()
    {
        // Show scrum board when state changed.
        controller.GameManager.UI.PlanningMenu.Hide();
        controller.GameManager.UI.ScrumMenu.Show();
        base.Destroy();
    }
}
