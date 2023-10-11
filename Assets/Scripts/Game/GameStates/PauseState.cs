using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseState : GameState
{
    ContextManager gameContext;

    public override void Handle(ContextManager _controller)
    {
        gameContext = _controller;
        base.Handle(gameContext);

        Pause();
    }

    public override void OnEscaped()
    {
        Resume();
    }

    public void Resume()
    {
        gameManager.UI.PauseMenu.Hide();
        Time.timeScale = 1f;
        gameManager.Context.Default();
    }

    private void Pause()
    {

        Time.timeScale = 0f;
        gameManager.UI.PauseMenu.Show();
    }
}
