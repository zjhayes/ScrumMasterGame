using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrumMenuController : MenuController
{
    [SerializeField]
    TaskDetailsPanel taskDetailsPanel;
    [SerializeField]
    Container toDoContainer;
    [SerializeField]
    Container inProgressContainer;
    [SerializeField]
    Container doneContainer;

    Dictionary<Task, TaskPanel> taskPanelCache;

    public override void SetUp()
    {
        // Set up panels.
        taskDetailsPanel.SetUp();
        LoadTaskPanels();
    }

    private void LoadTaskPanels()
    {
        // Add tasks to board.
        taskPanelCache = new Dictionary<Task, TaskPanel>();
        foreach (Task task in TaskManager.Instance.Tasks)
        {
            if (task.Status == TaskStatus.TO_DO)
            {
                TaskPanel taskPanel = UIManager.Instance.CreateTaskPanel(task, toDoContainer.gameObject.transform);
                taskPanel.onSelected += OnTaskPanelSelected; // Listen to task clicked, show details on click.
                taskPanelCache.Add(task, taskPanel);
            }
            else if (task.Status == TaskStatus.IN_PROGRESS)
            {
                TaskPanel taskPanel = UIManager.Instance.CreateTaskPanel(task, inProgressContainer.gameObject.transform);
                taskPanel.onSelected += OnTaskPanelSelected;
                taskPanelCache.Add(task, taskPanel);
            }
            else if (task.Status == TaskStatus.DONE)
            {
                TaskPanel taskPanel = UIManager.Instance.CreateTaskPanel(task, doneContainer.gameObject.transform);
                taskPanel.onSelected += OnTaskPanelSelected;
                taskPanelCache.Add(task, taskPanel);
            }
        }
    }

    public override void Escape()
    {
        // Don't close this window if sub-window open.
        if(taskDetailsPanel.IsShowing)
        {
            taskDetailsPanel.Escape();
        }
        else
        {
            base.Escape();
        }
    }

    void OnTaskPanelSelected(TaskPanel taskPanel)
    {
        taskDetailsPanel.Task = taskPanel.Task;
        taskDetailsPanel.Show();
    }

}
