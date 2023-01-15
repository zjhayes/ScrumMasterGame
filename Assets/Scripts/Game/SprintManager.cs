using UnityEngine;

[RequireComponent(typeof(SprintClock))]
public class SprintManager : Singleton<SprintManager>
{
    Sprint currentSprint;
    SprintClock clock;

    float sprintTime = 2.0f;

    public delegate void OnBeginPlanning();
    public OnBeginPlanning onBeginPlanning;

    public delegate void OnStartSprint();
    public OnStartSprint onStartSprint;

    protected override void Awake()
    {
        base.Awake();
        clock = GetComponent<SprintClock>();
        clock.TotalTime = sprintTime;
    }

    void Start()
    {
        BeginPlanning();
    }

    public void BeginPlanning()
    {
        onBeginPlanning?.Invoke();
    }

    public void StartSprint()
    {
        clock.Begin();
        onStartSprint?.Invoke();
    }
}
