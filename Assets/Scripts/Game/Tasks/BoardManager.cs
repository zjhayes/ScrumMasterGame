using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    Container taskContainer;
    [SerializeField]
    bool cacheInactive = false;

    List<Task> cachedTasks;

    public delegate void OnBoardUpdated();
    public event OnBoardUpdated onBoardUpdated;

    void Awake()
    {
        UpdateCache();
    }

    public List<Task> GetTasksWithStatus(TaskStatus status)
    {
        List<Task> tasksWithStatus = new List<Task>();
        foreach(Task task in Tasks)
        {
            if(task.Status == status)
            {
                tasksWithStatus.Add(task);
            }
        }
        return tasksWithStatus;
    }

    public List<Task> GetTasksWithAssignee(ICharacterController assignee)
    {
        List<Task> tasksWithAssignee = new List<Task>();
        foreach(Task task in Tasks)
        {
            if(task.Assignee == assignee)
            {
                tasksWithAssignee.Add(task);
            }
        }
        return tasksWithAssignee;
    }

    public Task GetFirstTaskWithAssignee(ICharacterController assignee)
    {
        foreach (Task task in Tasks)
        {
            if (task.Assignee == assignee)
            {
                return task;
            }
        }
        return null;
    }

    public Task GetFirstTaskWithStatusAndAssignee(ICharacterController assignee, TaskStatus status)
    {
        foreach (Task task in Tasks)
        {
            if (task.Assignee == assignee && task.Status == status)
            {
                return task;
            }
        }
        return null;
    }

    public void UpdateCache()
    {
        cachedTasks = taskContainer.Get<Task>(cacheInactive);
        onBoardUpdated?.Invoke();
    }

    public List<Task> Tasks
    {
        get
        {
            if (cachedTasks == null) { UpdateCache(); }
            return cachedTasks;
        }
    }
}
