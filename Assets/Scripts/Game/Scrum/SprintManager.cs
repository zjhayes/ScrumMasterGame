using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SprintClock))]
public class SprintManager : GameBehaviour
{
    [SerializeField]
    float sprintTime = 120.0f;

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
        clock.onExpiration += BeginRelease;
    }

    void Start()
    {
        BeginPlanning();
    }

    public void BeginPlanning()
    {
        // Create new Sprint.
        currentSprint = new Sprint();
        sprintHistory.Add(currentSprint);
        currentSprint.Number = sprintHistory.Count;

        OnBeginPlanning?.Invoke();
    }

    public void BeginSprint()
    {
        clock.Begin();
        OnBeginSprint?.Invoke();
    }

    public void BeginRelease()
    {
        currentSprint.CompleteTasks = gameManager.Board.GetTasksWithStatus(TaskStatus.DONE);
        currentSprint.IncompleteTasks = gameManager.Board.GetTasksWithStatus(TaskStatus.TO_DO, TaskStatus.IN_PROGRESS);
        gameManager.Board.ArchiveTasksWithStatus(TaskStatus.DONE);
        BeginRetrospective();
    }

    public void BeginRetrospective()
    {
        OnBeginRetrospective?.Invoke();
    }

    public void EndEarly()
    {
        BeginRelease();
    }

    public void EndSprintEarlyIfAllDone()
    {
        if (gameManager.Board.GetTasksWithStatus(TaskStatus.TO_DO, TaskStatus.IN_PROGRESS).Count <= 0)
        {
            EndEarly();
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
}
