using System.Collections;
using System.Collections.Generic;

public class Sprint
{
    private int sprintNumber;
    private List<Task> completeTasks;
    private List<Task> incompleteTasks;
    private float proficiency = 0f;
    private float remainingTime = 0f;

    public Sprint()
    {
        completeTasks = new List<Task>();
        incompleteTasks = new List<Task>();
    }

    public int Number
    {
        get { return sprintNumber; }
        set { sprintNumber = value; }
    }

    public List<Task> CompleteTasks
    {
        get { return completeTasks; }
        set { completeTasks = value; }
    }

    public List<Task> IncompleteTasks
    {
        get { return incompleteTasks; }
        set { incompleteTasks = value; }
    }

    public float Proficiency
    {
        get { return proficiency; }
        set { proficiency = value; }
    }

    public float RemainingTime
    {
        get { return remainingTime; }
        set { remainingTime = value; }
    }
}
