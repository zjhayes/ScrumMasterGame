/* A Story can represent any type of work, including tasks, bugs and feature requests. */
public class Story
{
    private StoryDetails details;
    private StoryStatus status;
    private ICharacterController assignee;
    private StoryOutcome outcome;
    private int storyPoints = 0;

    public event Events.CharacterEvent OnAssigneeChanged;
    public event Events.StoryEvent OnStatusChanged;

    public Story(StoryDetails details, StoryStatus status)
    {
        this.details = details;
        this.status = status;
        outcome = new StoryOutcome();
        storyPoints = details.Requirements.MinkowskiP3;
    }

    public StoryDetails Details
    {
        get { return details; }
    }

    public int StoryPoints
    {
        get { return storyPoints; }
    }

    public StoryStatus Status
    {
        get { return status; }
        set
        {
            status = value;
            OnStatusChanged?.Invoke(this);
        }
    }

    public ICharacterController Assignee
    {
        get { return assignee; }
        set
        {
            if (assignee != value)
            {
                assignee = value;
                OnAssigneeChanged?.Invoke(assignee);
            }
        }
    }

    public StoryOutcome Outcome
    {
        get { return outcome; }
    }
}
