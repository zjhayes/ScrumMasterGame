using UnityEngine;

[RequireComponent(typeof(ProductionStats))]
public class Task : MonoBehaviour, IContainable
{
    [SerializeField]
    private Sprite taskTypeIcon;
    [SerializeField]
    private string summary;
    [SerializeField]
    private string description;
    [SerializeField]
    private int storyPoints;
    [SerializeField]
    private ICharacterController assignee;
    [SerializeField]
    private TaskStatus status = TaskStatus.INACTIVE;

    private ProductionStats stats;
    private TaskOutcome outcome;

    private float completeness = 0f; // Percent of finished work.

    public event Events.CharacterEvent OnAssigneeChanged;
    public event Events.GameEvent OnStatusChanged;

    private void Awake()
    {
        stats = GetComponent<ProductionStats>();
        outcome = new TaskOutcome();
        UpdateEnablementBasedOnStatus();
    }

    public string Summary
    {
        get { return summary; }
    }

    public string Description
    {
        get { return description; }
    }

    public int StoryPoints
    {
        get { return storyPoints; }
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

    public TaskStatus Status
    {
        get { return status; }
        set
        {
            status = value;
            UpdateEnablementBasedOnStatus();
            OnStatusChanged?.Invoke();
        }
    }

    public ProductionStats Stats
    {
        get { return stats; }
    }

    public TaskOutcome Outcome
    {
        get { return outcome; }
    }

    public Sprite TaskTypeIcon
    {
        get { return taskTypeIcon; }
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

    private void UpdateEnablementBasedOnStatus()
    {
        // Disable when inactive or archived.
        if (status == TaskStatus.INACTIVE || status == TaskStatus.ARCHIVED)
        {
            this.enabled = false;
        }
        else
        {
            this.enabled = true;
        }
    }
}

public enum TaskStatus
{
    INACTIVE,
    BACKLOG,
    TO_DO,
    IN_PROGRESS,
    DONE,
    ARCHIVED
}

