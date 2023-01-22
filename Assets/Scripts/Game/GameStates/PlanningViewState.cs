using UnityEngine;

public class PlanningViewState : GameState
{
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;
        UIManager.Instance.ScrumMenu.Hide();
        UIManager.Instance.PlanningMenu.Show();
        controller.Camera.SwitchToBoardCamera();
    }

    public override void Escape()
    {
        // TODO: Show settings menu.
    }

    public override void Destroy()
    {
        // Show scrum board when state changed.
        UIManager.Instance.PlanningMenu.Hide();
        UIManager.Instance.ScrumMenu.Show();
        base.Destroy();
    }
}
