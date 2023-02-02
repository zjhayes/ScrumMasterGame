using UnityEngine;

public class DefaultState : GameState
{ 
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;

        controller.CurrentCharacter = null;
        controller.GameManager.Camera.SwitchToOverworldCamera();
    }

    public override void Escape()
    {
        // TODO: Show settings menu.
    }
}