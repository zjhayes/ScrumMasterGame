using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ProductionStats))]
public class Task : MonoBehaviour, IContainable
{
    [SerializeField]
    Sprite taskTypeIcon;
    [SerializeField]
    string summary;
    [SerializeField]
    string description;
    [SerializeField]
    int storyPoints;
    [SerializeField]
    ICharacterController assignee;
    [SerializeField]
    TaskStatus status = TaskStatus.INACTIVE;
    ProductionStats stats;

    float completeness = 0f; // Percent of finished work.

    public delegate void OnAssigneeChanged();
    public event OnAssigneeChanged onAssigneeChanged;

    public delegate void OnStatusChanged();
    public event OnStatusChanged onStatusChanged;

    void Awake()
    {
        stats = GetComponent<ProductionStats>();
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
            if(assignee != value)
            {
                assignee = value;
                onAssigneeChanged?.Invoke();
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
            onStatusChanged?.Invoke();
        }
    }

    public ProductionStats Stats
    {
        get { return stats; }
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
