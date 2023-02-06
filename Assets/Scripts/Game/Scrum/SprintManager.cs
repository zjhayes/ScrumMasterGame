using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SprintClock))]
public class SprintManager : MonoBehaviour
{
    [SerializeField]
    float sprintTime = 120.0f;

    int sprintNumber = 1;
    SprintClock clock;

    public delegate void OnBeginPlanning();
    public event OnBeginPlanning onBeginPlanning;

    public delegate void OnBeginSprint();
    public event OnBeginSprint onBeginSprint;

    public delegate void OnBeginRetrospective();
    public event OnBeginRetrospective onBeginRetrospective;

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

    public SprintClock Clock
    {
        get { return clock; }
    }
}