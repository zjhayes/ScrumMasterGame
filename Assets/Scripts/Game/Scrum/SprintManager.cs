using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SprintClock))]
public class SprintManager : MonoBehaviour
{
    [SerializeField]
    float sprintTime = 120.0f;
    [SerializeField]
    TaskManager taskManager;

    int sprintNumber = 1;
    SprintClock clock;

    public delegate void OnBeginPlanning();
    public OnBeginPlanning onBeginPlanning;

    public delegate void OnBeginSprint();
    public OnBeginSprint onBeginSprint;

    public delegate void OnBeginRetrospective();
    public OnBeginRetrospective onBeginRetrospective;

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
        onBeginPlanning?.Invoke();
    }

    public void BeginSprint()
    {
        clock.Begin();
        onBeginSprint?.Invoke();
    }

    public void BeginRetrospective()
    {
        sprintNumber++;
        onBeginRetrospective?.Invoke();
        //SceneManager.LoadScene(1); // Reload scene.
        
        BeginPlanning(); // TODO: Move this.
    }

    public TaskManager Board
    {
        get { return taskManager; }
    }

    public SprintClock Clock
    {
        get { return clock; }
    }
}
