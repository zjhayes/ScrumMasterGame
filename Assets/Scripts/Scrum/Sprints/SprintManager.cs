using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SprintClock))]
public class SprintManager : GameBehaviour
{
    [SerializeField]
    private float sprintTime = 120.0f;

    private Sprint currentSprint;
    private List<Sprint> sprintHistory;
    private SprintClock clock;

    public event Events.GameEvent OnBeginPlanning;
    public event Events.GameEvent OnBeginSprint;
    public event Events.GameEvent OnRelease;
    public event Events.GameEvent OnBeginRetrospective;

    void Awake()
    {
        sprintHistory = new List<Sprint>();
        clock = GetComponent<SprintClock>();
        clock.TotalTime = sprintTime;
        clock.OnExpiration += BeginRelease;
    }

    public void BeginPlanning()
    {
        clock.ResetTime();
        NextSprint();
        OnBeginPlanning?.Invoke();
    }

    public void BeginSprint()
    {
        clock.StartTime();
        OnBeginSprint?.Invoke();
    }

    public void BeginRelease()
    {
        OnRelease?.Invoke();
    }

    public void BeginRetrospective()
    {
        OnBeginRetrospective?.Invoke();
    }

    public void EndSprintEarly()
    {
        clock.Stop();
        BeginRelease();
    }

    public void EndSprintEarlyIfAllDone()
    {
        // Check if there are any incomplete stories in the sprint.
        if (gameManager.Board.Stories.WithStatus(StoryStatus.TO_DO, StoryStatus.IN_PROGRESS).Get().Count <= 0) // TODO: Move to GameState.
        {
            EndSprintEarly();
        }
    }

    public Sprint Current
    {
        get { return currentSprint; }
    }

    public SprintClock Clock
    {
        get { return clock; }
    }

    private void NextSprint()
    {
        // End game when no more sprints. TODO: Add End Game state.
        Debug.Log(gameManager.Board.Stories?.WithStatus(StoryStatus.BACKLOG).Get().Count);
        if (gameManager.Board.Stories?.WithStatus(StoryStatus.BACKLOG).Get().Count == 0)
        {
            Debug.Log("No more Stories, ending game.");
            gameManager.Quit();
            return;
        }

        // Set current sprint to next sprint.
        currentSprint = new Sprint();
        sprintHistory.Add(currentSprint);
        currentSprint.Number = sprintHistory.Count;
    }
}
