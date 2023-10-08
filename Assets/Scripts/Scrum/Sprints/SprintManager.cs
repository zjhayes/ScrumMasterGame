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
        NextSprint();
        OnBeginPlanning?.Invoke();
    }

    public void BeginSprint()
    {
        clock.Begin();
        OnBeginSprint?.Invoke();
    }

    public void BeginRelease()
    {
        BeginRetrospective(); // TODO: Add release state.
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
        // End game when no more sprints. TODO: Add quit option to settings instead.
        if (gameManager.Board.Stories?.Get().Count == 0)
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
