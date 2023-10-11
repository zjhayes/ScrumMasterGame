using UnityEngine;

public class DefaultState : GameState
{ 
    private ContextManager gameContext;

    public override void Handle(ContextManager _controller)
    {
        gameContext = _controller;

        gameContext.CurrentCharacter = null;
        gameManager.Camera.SwitchToOverworldCamera();
        base.Handle(gameContext);
    }

    public override void ChangeView()
    {
        // Enter Scrum Board view.
        gameContext.SwitchToScrumView();
    }

    public override void OnEscaped()
    {
        gameContext.Pause();
    }
}