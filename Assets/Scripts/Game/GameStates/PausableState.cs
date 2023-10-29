using HierarchicalStateMachine;
using UnityEngine;

public abstract class PausableState : EscapableState
{
    protected bool isPaused = false;

    public PausableState(IGameManager _gameManager, StateMachine _context) : base(_gameManager, _context){}

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        Resume();
    }

    protected override void Escape()
    {
        TogglePause();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Pause();
        }
        else
        {
            Resume();
        }
    }

    private void Resume()
    {
        // Resume previous state.
        gameManager.UI.PauseMenu.Hide();
        Time.timeScale = 1f;
    }

    private void Pause()
    {
        // Freeze game progression and show pause menu.
        Time.timeScale = 0f;
        gameManager.UI.PauseMenu.Show();
    }
}
