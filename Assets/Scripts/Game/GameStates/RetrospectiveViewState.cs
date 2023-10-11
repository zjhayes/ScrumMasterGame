using System.Collections.Generic;
using UnityEngine;

public class RetrospectiveViewState : GameState
{
    private ContextManager gameContext;

    public override void Handle(ContextManager _controller)
    {
        gameContext = _controller;
        
        gameManager.UI.ScrumMenu.Hide();
        gameManager.UI.StatusBar.Hide();
        gameManager.UI.RetrospectiveMenu.Show();
        gameManager.Camera.SwitchToBoardCamera();
        base.Handle(gameContext);
    }

    public override void OnEscaped()
    {
        gameContext.Pause();
    }

    public override void Exit()
    {
        // Show scrum board when state changed. // TODO: Delete?
        gameManager.UI.RetrospectiveMenu.Hide();
        gameManager.UI.ScrumMenu.Show();
        gameManager.UI.StatusBar.Show();
        base.Exit();
    }
}
