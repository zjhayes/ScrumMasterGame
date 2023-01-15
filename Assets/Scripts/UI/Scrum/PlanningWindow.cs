using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanningWindow : MenuController
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
    
    public override void SetUp()
    {
        // Set up panels.
        taskDetailsPanel.SetUp();
        sprintDetailsPanel.SetUp();
        inSprintPanel.SetUp();

        taskDetailsPanel.onShow += OnShowTaskDetails;
        taskDetailsPanel.onHide += OnHideTaskDetails;

        // Add tasks to board.
        taskPanelCache = new Dictionary<Task, TaskPanel>();
        foreach(Task task in TaskManager.Instance.Tasks)
        {
            if (task.Status == TaskStatus.BACKLOG)
            {
                TaskPanel taskPanel = UIManager.Instance.CreateTaskPanel(task, backlogContainer.gameObject.transform);
                taskPanel.onSelected += OnTaskPanelSelected; // Listen to task clicked, show details on click.
                taskPanelCache.Add(task, taskPanel);
            }
            else if(task.Status == TaskStatus.TO_DO || task.Status == TaskStatus.IN_PROGRESS)
            {
                TaskPanel taskPanel = UIManager.Instance.CreateTaskPanel(task, inSprintPanel.Container.gameObject.transform);
                taskPanelCache.Add(task, taskPanel);
            }
        }

        SprintManager.Instance.onBeginPlanning += Show;
        SprintManager.Instance.onBeginSprint += Hide;
        sprintDetailsPanel.onBeginSprint += OnBeginSprint;
        taskDetailsPanel.onAddToSprint += OnAddToSprint;
        taskDetailsPanel.onRemoveFromSprint += OnRemoveFromSprint;
        base.SetUp();
    }

    void OnTaskPanelSelected(TaskPanel taskPanel)
    {
        taskDetailsPanel.Task = taskPanel.Task;
        taskDetailsPanel.Show();
    }

    void OnShowTaskDetails(MenuController taskPanel)
    {
        sprintDetailsPanel.Minify();
    }

    void OnHideTaskDetails(MenuController taskPanel)
    {
        sprintDetailsPanel.Expand();
    }

    void OnAddToSprint(Task task)
    {
        taskDetailsPanel.Hide();
        TaskPanel taskPanel = taskPanelCache[task];
        inSprintPanel.Container.Add(taskPanel);
    }

    void OnRemoveFromSprint(Task task)
    {
        taskDetailsPanel.Hide();
        TaskPanel taskPanel = taskPanelCache[task];
        backlogContainer.Add(taskPanel);
    }

    void OnBeginSprint()
    {
        SprintManager.Instance.BeginSprint();
    }
}
