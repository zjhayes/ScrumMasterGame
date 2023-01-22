using UnityEngine;

[RequireComponent(typeof(SprintClock))]
public class SprintManager : Singleton<SprintManager>
{
    int sprintNumber = 1;
    float sprintTime = 120.0f;
    SprintClock clock;

    public delegate void OnBeginPlanning();
    public OnBeginPlanning onBeginPlanning;

    public delegate void OnBeginSprint();
    public OnBeginSprint onBeginSprint;

    public delegate void OnBeginRetrospective();
    public OnBeginRetrospective onBeginRetrospective;

    protected override void Awake()
    {
        base.Awake();
        clock = GetComponent<SprintClock>();
        clock.TotalTime = sprintTime;
        clock.onExpiration += BeginRetrospective;
    }

    void Start()
    {
        BeginPlanning();
    }

    public void BeginPlanning()
    {
        ContextManager.Instance.SwitchToPlanningView();
        onBeginPlanning?.Invoke();
    }

    public void BeginSprint()
    {
        ContextManager.Instance.Default();
        clock.Begin();
        onBeginSprint?.Invoke();
    }

    public void BeginRetrospective()
    {
        sprintNumber++;
        onBeginRetrospective?.Invoke();
        BeginPlanning(); // TODO: Move this.
    }

    public SprintClock Clock
    {
        get { return clock; }
    }
}
