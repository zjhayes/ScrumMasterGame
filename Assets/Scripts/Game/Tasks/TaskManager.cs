using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : Singleton<TaskManager>
{
    [SerializeField]
    Container taskContainer;
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

    public List<Task> Tasks
    {
        get
        {
            return taskContainer.Get<Task>();
        }
    }
}
