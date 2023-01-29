using UnityEngine;

public class DefaultState : GameState
{ 
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;

        controller.Camera.SwitchToOverworldCamera();

        // Player can toggle board view.
        PlayerControls.Instance.onShowBoard += controller.ShowScrumBoard;
    }

    public override void Escape()
    {
        // TODO: Show settings menu.
    }

    public override void Destroy()
    {
        PlayerControls.Instance.onShowBoard -= controller.ShowScrumBoard;
    }
}