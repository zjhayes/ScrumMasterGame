using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrospectiveViewState : GameState
{
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;
        ApplyProductionStats();
        gameManager.UI.ScrumMenu.Hide();
        gameManager.UI.StatusBar.Hide();
        gameManager.UI.RetrospectiveMenu.Show();
        gameManager.Camera.SwitchToBoardCamera();
        base.Handle(controller);
    }

    public override void OnEscaped()
    {
        // TODO: Show settings menu.
    }

    public override void Exit()
    {
        // Show scrum board when state changed.
        gameManager.UI.RetrospectiveMenu.Hide();
        gameManager.UI.ScrumMenu.Show();
        gameManager.UI.StatusBar.Show();
        base.Exit();
    }

    private void ApplyProductionStats()
    {
        float chanceOfErrors = 0f;
        List<Task> completedTasks = gameManager.Sprint.Current.CompleteTasks;
        foreach (Task task in completedTasks)
        {
            // Add task requirements to production stats.
            gameManager.Production.Stats.Add(task.Stats);
            chanceOfErrors += Mathf.Clamp(task.Outcome.ChanceOfErrors, Numeric.ZERO, Numeric.ONE_HUNDRED_PERCENT);
        }
        // Capture proficiency.
        if (completedTasks.Count > 0)
        {
            gameManager.Sprint.Current.Proficiency = Numeric.ONE_HUNDRED_PERCENT - (chanceOfErrors / completedTasks.Count);
        }
        else
        {
            gameManager.Sprint.Current.Proficiency = Numeric.ZERO; 
        }

        // Capture remaining time.
        gameManager.Sprint.Current.RemainingTime = gameManager.Sprint.Clock.CurrentTime;
    }
}
