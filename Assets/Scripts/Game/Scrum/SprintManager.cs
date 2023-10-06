using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(SprintClock))]
public class SprintManager : GameBehaviour
{
    [SerializeField]
    private float sprintTime = 120.0f;
    [SerializeField]
    private List<SprintDetails> sprintDetails;

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

    void Start()
    {
        BeginPlanning();
    }

    public void BeginPlanning()
    {
        // Create new Sprint.
        currentSprint = NextSprint();
        
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
        BeginRetrospective(); // TODO: Add release state.
    }

    public void BeginRetrospective()
    {
        OnBeginRetrospective?.Invoke();
    }

    public void EndEarly()
    {
        clock.Stop();
        BeginRelease();
    }

    public void EndSprintEarlyIfAllDone()
    {
        // Check if there are any incomplete stories in the sprint.
        if (gameManager.Board.Stories.WithStatus(StoryStatus.TO_DO, StoryStatus.IN_PROGRESS).Get().Count <= 0) // TODO: Move to GameState.
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

    private Sprint NextSprint()
    {
        // End game when no more sprints.
        if(sprintHistory.Count == sprintDetails.Count)
        {
            gameManager.Quit();
        }

        // Return next sprint from sprint details.
        return new Sprint(sprintDetails[sprintHistory.Count]);
    }
}
