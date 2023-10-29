using HierarchicalStateMachine;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseState : GameState
{
    public ReleaseState(IGameManager _gameManager, StateMachine _context) : base(_gameManager, _context) {}

    public override void Enter()
    {
        ReleaseSprint();
        UpdateBoard();
        gameManager.Sprint.BeginRetrospective(); // Immediately go to retrospective.
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

        foreach (Story story in completedTasks)
        {
            // Add task requirements to production stats.
            gameManager.Production.Stats.Add(story.Details.Requirements);

            // Determine if task has defect.
            if (HasDefect(story.Outcome) && story.Details.Risk)
            {
                // Add defect to board.
                gameManager.Sprint.Current.Defects.Add(story.Details.Risk);

            }
            // Accumulate chance of errors to calculate quality.
            chanceOfErrors += Mathf.Clamp(story.Outcome.ChanceOfErrors, Numeric.ZERO, Numeric.ONE_HUNDRED_PERCENT);
            // Accumulate cycle time to calculate average.
            cycleTimeSum += story.Outcome.CycleTime;
        }

        // Capture quality based on chance of errors, and cycle time per story point.
        if (completedTasks.Count > 0)
        {
            gameManager.Sprint.Current.Quality = Numeric.ONE_HUNDRED_PERCENT - (chanceOfErrors / completedTasks.Count);
            gameManager.Sprint.Current.CycleTime = cycleTimeSum / StoryService.CountStoryPoints(completedTasks);
        }
        else
        {
            gameManager.Sprint.Current.Quality = Numeric.ZERO;
            gameManager.Sprint.Current.CycleTime = Numeric.ZERO;
        }

        // Capture remaining time.
        gameManager.Sprint.Current.RemainingTime = gameManager.Sprint.Clock.CurrentTime;

        // Update number of users. Scales with quality, availability and required functionality.
        gameManager.Sprint.Current.NewUserCount = (int)(gameManager.Sprint.Current.Quality * gameManager.Production.Availability * gameManager.Production.Stats.Functionality / gameManager.Production.Stats.Maximum);
        gameManager.Production.UserCount += gameManager.Sprint.Current.NewUserCount;
    }

    private bool HasDefect(StoryOutcome outcome)
    {
        float chanceOfErrors = outcome.ChanceOfErrors;
        float randomNumber = Random.Range(0f, 100f);

        // Compare the random number to chanceOfErrors, no defect if chance of errors is greater.
        return randomNumber <= chanceOfErrors;
    }

    private void UpdateBoard()
    {
        // Get new stories caused by completed stories, add to backlog.
        List<StoryDetails> newStoryDetails = new();
        foreach (Story story in gameManager.Board.Stories.WithStatus(StoryStatus.DONE).Get())
        {
            newStoryDetails.AddRange(story.Details.Causes);
        }
        newStoryDetails.AddRange(gameManager.Sprint.Current.Defects); // Add bugs.
        gameManager.Board.ImportStoryDetails(newStoryDetails);
        gameManager.Board.RemoveStoriesWithStatus(StoryStatus.DONE); // Remove completed stories from board.
    }
}
