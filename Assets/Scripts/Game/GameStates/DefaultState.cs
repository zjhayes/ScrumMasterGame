using UnityEngine;

public class DefaultState : GameState
{ 
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;

        controller.CurrentCharacter = null;
        gameManager.Camera.SwitchToOverworldCamera();
        base.Handle(controller);
    }

    public override void ChangeView()
    {
        // Enter Scrum Board view.
        controller.SwitchToScrumView();
    }

    public override void OnEscaped()
    {
        // TODO: Show settings menu.
    }
}