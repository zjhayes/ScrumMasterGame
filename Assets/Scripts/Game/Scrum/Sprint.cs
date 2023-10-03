using System.Collections.Generic;

public class Sprint
{
    private int sprintNumber;
    private List<Task> completeTasks;
    private List<Task> incompleteTasks;
    private float quality = 0f;
    private float averageCycleTime = 0f;
    private float remainingTime = 0f;
    private int numberOfDefects = 0;

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

    public float Quality
    {
        get { return quality; }
        set { quality = value; }
    }

    public float CycleTime
    {
        get { return averageCycleTime; }
        set { averageCycleTime = value; }
    }

    public float RemainingTime
    {
        get { return remainingTime; }
        set { remainingTime = value; }
    }

    public int Defects
    {
        get { return numberOfDefects; }
        set { numberOfDefects = value; }
    }
}
