using System.Collections.Generic;

public class Sprint
{
    private SprintDetails sprintDetails;
    private int sprintNumber;
    private List<Story> completeTasks;
    private List<Story> incompleteTasks;
    private float quality = 0f;
    private float averageCycleTime = 0f;
    private float remainingTime = 0f;
    private int numberOfDefects = 0;

    public Sprint(SprintDetails sprintDetails)
    {
        this.sprintDetails = sprintDetails;
        completeTasks = new List<Story>();
        incompleteTasks = new List<Story>();
    }

    public SprintDetails Details
    {
        get { return sprintDetails; }
    }

    public int Number
    {
        get { return sprintNumber; }
        set { sprintNumber = value; }
    }

    public List<Story> CompleteTasks
    {
        get { return completeTasks; }
        set { completeTasks = value; }
    }

    public List<Story> IncompleteTasks
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
