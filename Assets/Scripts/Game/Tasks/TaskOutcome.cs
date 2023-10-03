using UnityEngine;

public class TaskOutcome
{
    private float chanceOfErrors;
    private float startTime;
    private float endTime;

    public TaskOutcome()
    {
        chanceOfErrors = Numeric.ONE_HUNDRED_PERCENT;
    }

    public float ChanceOfErrors
    {
        get { return chanceOfErrors; }
        set { chanceOfErrors = value; }
    }

    public float StartTime
    {
        get { return startTime; }
        set { startTime = value; }
    }

    public float EndTime
    {
        get { return endTime; }
        set { endTime = value; }
    }

    public float CycleTime
    {
        get { return startTime - endTime; }
    }
}
