using UnityEngine;

public class TaskOutcome
{
    private float chanceOfErrors;

    public TaskOutcome()
    {
        chanceOfErrors = Numeric.ONE_HUNDRED_PERCENT;
    }

    public float ChanceOfErrors
    {
        get { return chanceOfErrors; }
        set { chanceOfErrors = value; }
    }
}
