using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    Container taskContainer;
    [SerializeField]
    bool cacheInactive = false;

    List<Task> cachedTasks;

    void Awake()
    {
        // Keep alive for duration of game.
        DontDestroyOnLoad(gameObject);
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

    public void UpdateCache()
    {
        cachedTasks = new List<Task>();

        cachedTasks = taskContainer.Get<Task>(cacheInactive);
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
