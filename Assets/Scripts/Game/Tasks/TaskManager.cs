using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : Singleton<TaskManager>
{
    public List<Task> GetTasksWithStatus(TaskStatus status)
    {
        List<Task> tasksWithStatus = new List<Task>();
        foreach(Task task in GetAllTasks())
        {
            if(task.Status == status)
            {
                tasksWithStatus.Add(task);
            }
        }
        return tasksWithStatus;
    }

    public Task[] GetAllTasks()
    {
        return GetComponentsInChildren<Task>();
    }
}
