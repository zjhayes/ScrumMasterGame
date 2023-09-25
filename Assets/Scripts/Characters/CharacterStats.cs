using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public static int MINIMUM = 0;
    public static int MAXIMUM = 10;

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
    }

    public int Backend
    {
        get { return backend; }
    }

    public int ProblemSolving
    {
        get { return problemSolving; }
    }

    public int TimeManagement
    {
        get { return timeManagement; }
    }
}
