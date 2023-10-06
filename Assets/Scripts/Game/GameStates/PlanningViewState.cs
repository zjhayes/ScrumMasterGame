using UnityEngine;

public class PlanningViewState : GameState
{
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;

        UpdateBoard();
        UpdateView();
        base.Handle(controller);
    }

    public override void OnEscaped()
    {
        // TODO: Show settings menu.
    }

    public override void Exit()
    {
        // Show scrum board when state changed.
        gameManager.UI.PlanningMenu.Hide();
        gameManager.UI.ScrumMenu.Show();
        gameManager.UI.StatusBar.Show();
        base.Exit();
    }

    private void UpdateBoard()
    {
        gameManager.Board.ImportStoryDetails(gameManager.Sprint.Current.Details.Stories);
        gameManager.Board.RemoveStoriesWithStatus(StoryStatus.DONE);
    }

    private void UpdateView()
    {
        gameManager.UI.ScrumMenu.Hide();
        gameManager.UI.StatusBar.Hide();
        gameManager.UI.PlanningMenu.Show();
        gameManager.Camera.SwitchToBoardCamera();
    }
}
