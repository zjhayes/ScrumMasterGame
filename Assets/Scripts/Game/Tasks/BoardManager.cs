using System.Collections.Generic;
using System;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private Container taskContainer;
    [SerializeField]
    private bool cacheInactive = false;

    private List<Task> cachedTasks;

    public event Events.GameEvent OnBoardUpdated;

    private void Awake()
    {
        UpdateCache();
    }

    public List<Task> GetTasksWithStatus(params TaskStatus[] statuses)
    {
        List<Task> tasksWithStatus = new();
        foreach (Task task in Tasks)
        {
            if (Array.Exists(statuses, status => status == task.Status))
            {
                tasksWithStatus.Add(task);
            }
        }
        return tasksWithStatus;
    }

    public List<Task> GetTasksWithAssignee(ICharacterController assignee)
    {
        List<Task> tasksWithAssignee = new();
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

    public bool TryGetFirstTaskWithStatusAndAssignee(ICharacterController assignee, TaskStatus status, out Task taskWithStatusAndAssignee)
    {
        foreach (Task task in Tasks)
        {
            if (task.Assignee == assignee && task.Status == status)
            {
                taskWithStatusAndAssignee = task;
                return true; // Task found.
            }
        }
        taskWithStatusAndAssignee = null;
        return false; // None found.
    }

    public int CountStoryPoints(List<Task> tasks)
    {
        int total = 0;
        foreach(Task task in tasks)
        {
            total += task.StoryPoints;
        }
        return total;
    }

    public int CountCharacterStoryPoints(ICharacterController character)
    {
        int total = 0;
        foreach (Task task in Tasks)
        {
            if (task.Assignee == character)
            {
                total += task.Stats.Total;
            }
        }
        return total;
    }

    public void ArchiveTasksWithStatus(TaskStatus status)
    {
        foreach (Task task in Tasks)
        {
            if (task.Status == status)
            {
                task.Status = TaskStatus.ARCHIVED;
            }
        }
    }

    public void UpdateCache()
    {
        cachedTasks = taskContainer.Get<Task>(cacheInactive);
        OnBoardUpdated?.Invoke();
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
