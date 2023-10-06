/* The status and results of a task. */
public class StoryOutcome
{
    private float completeness = 0f; // Percent of finished work.
    private float chanceOfErrors;
    private float startTime;
    private float endTime;

    public StoryOutcome()
    {
        chanceOfErrors = Numeric.ONE_HUNDRED_PERCENT;
    }

    public float Completeness
    {
        get { return completeness; }
        set { completeness = value; }
    }

    public bool IsReadyForProduction
    {
        get { return completeness >= Numeric.ONE_HUNDRED_PERCENT; }
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
