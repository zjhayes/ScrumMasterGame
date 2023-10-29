using UnityEngine;

public abstract class PausableState : EscapableState
{
    protected bool isPaused = false;

    public PausableState(IGameManager _gameManager) : base(_gameManager){}

    public override void Enter()
    {
        gameManager.UI.PauseMenu.ResumeButton.OnClick += Resume;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        gameManager.UI.PauseMenu.ResumeButton.OnClick -= Resume;
        Resume();
    }

    protected override void Escape()
    {
        TogglePause();
    }

    private void TogglePause()
    {
        if (!isPaused)
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
        isPaused = false;
    }

    private void Pause()
    {
        // Freeze game progression and show pause menu.
        Time.timeScale = 0f;
        gameManager.UI.PauseMenu.Show();
        isPaused = true;
    }
}
