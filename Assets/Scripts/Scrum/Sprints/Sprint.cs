using System.Collections.Generic;

public class Sprint
{
    private int sprintNumber;
    private List<Story> completeTasks;
    private List<Story> incompleteTasks;
    private float quality = 0f;
    private float averageCycleTime = 0f;
    private float remainingTime = 0f;
    private List<StoryDetails> defects;

    public Sprint()
    {
        completeTasks = new List<Story>();
        incompleteTasks = new List<Story>();
        defects = new List<StoryDetails>();
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

    public List<StoryDetails> Defects
    {
        get { return defects; }
    }
}
