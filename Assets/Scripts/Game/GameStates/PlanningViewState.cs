using UnityEngine;

public class PlanningViewState : GameState
{
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;
        
        GameManager.Instance.UI.ScrumMenu.Hide();
        GameManager.Instance.UI.PlanningMenu.Show();
        GameManager.Instance.Camera.SwitchToBoardCamera();
    }

    public override void Escape()
    {
        // TODO: Show settings menu.
    }

    public override void Destroy()
    {
        // Show scrum board when state changed.
        GameManager.Instance.UI.PlanningMenu.Hide();
        GameManager.Instance.UI.ScrumMenu.Show();
        base.Destroy();
    }
}
