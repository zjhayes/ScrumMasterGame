using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SprintClock))]
public class SprintManager : MonoBehaviour
{
    [SerializeField]
    float sprintTime = 120.0f;

    int sprintNumber = 1;
    SprintClock clock;

    public event Events.GameEvent OnBeginPlanning;
    public event Events.GameEvent OnBeginSprint;
    public event Events.GameEvent OnBeginRetrospective;

    void Awake()
    {
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
        OnBeginPlanning?.Invoke();
    }

    public void BeginSprint()
    {
        clock.Begin();
        OnBeginSprint?.Invoke();
    }

    public void BeginRetrospective()
    {
        sprintNumber++;
        OnBeginRetrospective?.Invoke();
        //SceneManager.LoadScene(1); // Reload scene.
        
        BeginPlanning(); // TODO: Move this.
    }

    public SprintClock Clock
    {
        get { return clock; }
    }
}
