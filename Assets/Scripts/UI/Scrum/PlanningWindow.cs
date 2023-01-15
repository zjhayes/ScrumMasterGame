using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanningWindow : MonoBehaviour
{
    [SerializeField]
    Container backlogContainer;
    [SerializeField]
    TaskDetailsPanel taskDetailsPanel;
    [SerializeField]
    SprintDetailsPanel sprintDetailsPanel;
    [SerializeField]
    InSprintPanel inSprintPanel;

    Dictionary<Task, TaskPanel> taskPanelCache;
    
    void Start()
    {
        // Add tasks to board.
        taskPanelCache = new Dictionary<Task, TaskPanel>();
        foreach(Task task in TaskManager.Instance.Tasks)
        {
            if (task.Status == TaskStatus.BACKLOG)
            {
                TaskPanel taskPanel = UIManager.Instance.CreateTaskPanel(task, backlogContainer.gameObject.transform);
                taskPanelCache.Add(task, taskPanel);
            }
            else if(task.Status == TaskStatus.TO_DO || task.Status == TaskStatus.IN_PROGRESS)
            {
                TaskPanel taskPanel = UIManager.Instance.CreateTaskPanel(task, inSprintPanel.Container.gameObject.transform);
                taskPanelCache.Add(task, taskPanel);
            }
        }

        taskDetailsPanel.onAddToSprint += OnAddToSprint;
        taskDetailsPanel.onRemoveFromSprint += OnRemoveFromSprint;
    }

    public void ShowTaskDetails(Task task)
    {
        taskDetailsPanel.Task = task;

        if(taskDetailsPanel.IsShowing)
        {
            taskDetailsPanel.UpdateDetails();
            taskDetailsPanel.UpdateAssignee();
        }
        else
        {
            taskDetailsPanel.Show();
        }

        inSprintPanel.Minify();
    }

    private void OnAddToSprint(Task task)
    {
        taskDetailsPanel.Hide();
        inSprintPanel.Expand();
        TaskPanel taskPanel = taskPanelCache[task];
        inSprintPanel.Container.Add(taskPanel);
    }

    private void OnRemoveFromSprint(Task task)
    {
        taskDetailsPanel.Hide();
        inSprintPanel.Expand();
        TaskPanel taskPanel = taskPanelCache[task];
        backlogContainer.Add(taskPanel);
    }
}
