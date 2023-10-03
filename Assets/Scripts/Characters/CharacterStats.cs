using UnityEngine;

public class CharacterStats : MonoBehaviour
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

    public int Frontend
    {
        get { return frontend; }
        set { frontend = Mathf.Clamp(value, MINIMUM, MAXIMUM); }
    }

    public int Backend
    {
        get { return backend; }
        set { backend = Mathf.Clamp(value, MINIMUM, MAXIMUM); }
    }

    public int ProblemSolving
    {
        get { return problemSolving; }
        set { problemSolving = Mathf.Clamp(value, MINIMUM, MAXIMUM); }
    }

    public int TimeManagement
    {
        get { return timeManagement; }
        set { timeManagement = Mathf.Clamp(value, MINIMUM, MAXIMUM); }
    }

    public int Velocity
    {
        get { return frontend + backend + problemSolving + timeManagement; }
    }
}
