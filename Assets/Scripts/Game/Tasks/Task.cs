using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ProductionStats))]
public class Task : MonoBehaviour
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
    CharacterController assignee;
    [SerializeField]
    Cartridge cartridge;
    [SerializeField]
    TaskStatus status = TaskStatus.INACTIVE;
    ProductionStats stats;

    void Awake()
    {
        stats = GetComponent<ProductionStats>();
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

    public CharacterController Assignee
    {
        get { return assignee; }
    }

    public TaskStatus Status
    {
        get { return status; }
        set { status = value; }
    }

    public ProductionStats Stats
    {
        get { return stats; }
    }

    public Sprite TaskTypeIcon
    {
        get { return taskTypeIcon; }
    }

    public Cartridge Cartridge
    {
        get { return cartridge; }
        set { cartridge = value; }
    }

}

public enum TaskStatus
{
    INACTIVE,
    BACKLOG,
    TO_DO,
    IN_PROGRESS,
    DONE
}
