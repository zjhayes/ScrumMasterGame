using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetrospectiveViewState : GameState
{
    private ContextManager controller;

    public override void Handle(ContextManager _controller)
    {
        controller = _controller;
        ReleaseSprint();
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

    private void ReleaseSprint()
    {
        // Capture complete/incomplete tasks.
        List<Story> completedTasks = gameManager.Board.Stories.WithStatus(StoryStatus.DONE).Get();
        gameManager.Sprint.Current.CompleteTasks = completedTasks;
        gameManager.Sprint.Current.IncompleteTasks = gameManager.Board.Stories.WithStatus(StoryStatus.TO_DO, StoryStatus.IN_PROGRESS).Get();

        // Capture sprint outcomes.
        float chanceOfErrors = 0f;
        float cycleTimeSum = 0f;
        int defects = 0;

        foreach (Story task in completedTasks)
        {
            // Add task requirements to production stats.
            gameManager.Production.Stats.Add(task.Details.Requirements);

            // Determine number of defects.
            if(CreatesDefect(task.Outcome))
            {
                defects++;
            }
            // Accumulate chance of errors to calculate quality.
            chanceOfErrors += Mathf.Clamp(task.Outcome.ChanceOfErrors, Numeric.ZERO, Numeric.ONE_HUNDRED_PERCENT);
            // Accumulate cycle time to calculate average.
            cycleTimeSum += task.Outcome.CycleTime;
        }

        // Capture quality based on chance of errors.
        if (completedTasks.Count > 0)
        {
            gameManager.Sprint.Current.Quality = Numeric.ONE_HUNDRED_PERCENT - (chanceOfErrors / completedTasks.Count);
            gameManager.Sprint.Current.CycleTime = cycleTimeSum / completedTasks.Count;
        }
        else
        {
            gameManager.Sprint.Current.Quality = Numeric.ZERO;
            gameManager.Sprint.Current.CycleTime = Numeric.ZERO;
        }

        // Capture defect rate.
        gameManager.Sprint.Current.Defects = defects;

        // Capture remaining time.
        gameManager.Sprint.Current.RemainingTime = gameManager.Sprint.Clock.CurrentTime;
    }

    private bool CreatesDefect(StoryOutcome outcome)
    {
        float chanceOfErrors = outcome.ChanceOfErrors;
        float randomNumber = Random.Range(0f, 100f);
        
        // Compare the random number to chanceOfErrors, no defect if chance of errors is greater.
        return randomNumber <= chanceOfErrors;
    }
}
