using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    public const int MINIMUM = Numeric.ZERO;
    public const int MAXIMUM = 10;

    [SerializeField]
    [Range(0,10)]
    private int frontend;

    [SerializeField]
    [Range(0, 10)]
    private int backend;

    [SerializeField]
    [Range(0, 10)]
    private int problemSolving;

    [SerializeField]
    [Range(0, 10)]
    private int timeManagement;

    public Events.CharacterStatEvent OnStatUpdated;

    public int Frontend
    {
        get { return frontend; }
        set 
        { 
            frontend = Mathf.Clamp(value, MINIMUM, MAXIMUM);
            OnStatUpdated?.Invoke(CharacterStat.FRONTEND);
        }
    }

    public int Backend
    {
        get { return backend; }
        set 
        { 
            backend = Mathf.Clamp(value, MINIMUM, MAXIMUM);
            OnStatUpdated?.Invoke(CharacterStat.BACKEND);
        }
    }

    public int ProblemSolving
    {
        get { return problemSolving; }
        set 
        { 
            problemSolving = Mathf.Clamp(value, MINIMUM, MAXIMUM);
            OnStatUpdated?.Invoke(CharacterStat.PROBLEM_SOLVING);
        }
    }

    public int TimeManagement
    {
        get { return timeManagement; }
        set 
        { 
            timeManagement = Mathf.Clamp(value, MINIMUM, MAXIMUM);
            OnStatUpdated?.Invoke(CharacterStat.TIME_MANAGEMENT);
        }
    }

    public int Velocity
    {
        get { return frontend + backend + problemSolving + timeManagement; }
    }

    public void Add(CharacterStats other)
    {
        Frontend = Mathf.Clamp(Frontend + other.Frontend, MINIMUM, MAXIMUM);
        Backend = Mathf.Clamp(Backend + other.Backend, MINIMUM, MAXIMUM);
        ProblemSolving = Mathf.Clamp(ProblemSolving + other.ProblemSolving, MINIMUM, MAXIMUM);
        TimeManagement = Mathf.Clamp(TimeManagement + other.TimeManagement, MINIMUM, MAXIMUM);
    }

    public void Subtract(CharacterStats other)
    {
        Frontend = Mathf.Clamp(Frontend - other.Frontend, MINIMUM, MAXIMUM);
        Backend = Mathf.Clamp(Backend - other.Backend, MINIMUM, MAXIMUM);
        ProblemSolving = Mathf.Clamp(ProblemSolving - other.ProblemSolving, MINIMUM, MAXIMUM);
        TimeManagement = Mathf.Clamp(TimeManagement - other.TimeManagement, MINIMUM, MAXIMUM);
    }

    public void TakeLargest(CharacterStats other)
    {
        Frontend = Mathf.Clamp(Mathf.Max(Frontend, other.Frontend), MINIMUM, MAXIMUM);
        Backend = Mathf.Clamp(Mathf.Max(Backend, other.Backend), MINIMUM, MAXIMUM);
        ProblemSolving = Mathf.Clamp(Mathf.Max(ProblemSolving, other.ProblemSolving), MINIMUM, MAXIMUM);
        TimeManagement = Mathf.Clamp(Mathf.Max(TimeManagement, other.TimeManagement), MINIMUM, MAXIMUM);
    }
}
