using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlanningMenuController : MenuController
{
    [SerializeField]
    TaskDetailsPanel taskDetailsPanel;
    [SerializeField]
    SprintDetailsPanel sprintDetailsPanel;
    [SerializeField]
    Container backlogContainer;
    [SerializeField]
    Container inSprintContainer;
    [SerializeField]
    GameObject taskPanelPrefab;

    Dictionary<Task, TaskPanel> taskPanelCache;
    
    public override void SetUp()
    {
        // Set up panels.
        taskDetailsPanel.SetUp();
        taskDetailsPanel.Hide(); // Hidden by default.
        taskDetailsPanel.onHide += OnHideTaskDetails;

        sprintDetailsPanel.SetUp();

        ValidateSprintReadiness();

        sprintDetailsPanel.onBeginSprint += OnBeginSprintPressed;
        base.SetUp();
    }

    public override void Show()
    {
        LoadTaskPanels();
        base.Show();
    }

    public override void Hide()
    {
        if (taskDetailsPanel.IsShowing)
        {
            taskDetailsPanel.Hide();
        }
        base.Hide();
    }

    public override void Escape()
    {
        if (taskDetailsPanel.IsShowing)
        {
            taskDetailsPanel.Escape();
        }
        else
        {
            base.Escape(); // Planning Menu is not escapable.
        }
    }

    void OnTaskPanelSelected(TaskPanel taskPanel)
    {
        ShowPreviouslySelectedTaskPanel(); // Show previously selected task, if any.
        taskDetailsPanel.Task = taskPanel.Task;
        // Replace task panel with task details panel.
        taskDetailsPanel.gameObject.transform.SetParent(taskPanel.gameObject.transform.parent);
        taskDetailsPanel.gameObject.transform.SetSiblingIndex(taskPanel.gameObject.transform.GetSiblingIndex());
        taskPanel.Hide();
        taskDetailsPanel.Show();
    }

    void OnHideTaskDetails(MenuController taskDetails)
    {
        // Show hidden task panel.
        ShowPreviouslySelectedTaskPanel();
    }

    void ShowPreviouslySelectedTaskPanel()
    {
        // Check if there's a selected task, then show it.
        if (taskDetailsPanel.Task && taskPanelCache.ContainsKey(taskDetailsPanel.Task))
        {
            // Get and show task panel for current task in task details panel.
            TaskPanel selectedTaskPanel = taskPanelCache[taskDetailsPanel.Task];
            selectedTaskPanel.Show();
        }
    }

    void OnBeginSprintPressed()
    {
        SprintManager.Instance.BeginSprint();
    }

    void LoadTaskPanels()
    {
        // Clear existing task panels.
        if (taskPanelCache != null)
        {
            ClearBoard();
        }
        // Add tasks to board.
        taskPanelCache = new Dictionary<Task, TaskPanel>();
        foreach (Task task in TaskManager.Instance.Tasks)
        {
            if (task.Status == TaskStatus.BACKLOG)
            {
                TaskPanel taskPanel = CreateTaskPanel(task, backlogContainer.gameObject.transform);
                taskPanel.onSelected += OnTaskPanelSelected; // Listen to task clicked, show details on click.
                taskPanelCache.Add(task, taskPanel);
                task.onAssigneeChanged += UpdateSelectedTaskPanel;
            }
            else if (task.Status == TaskStatus.TO_DO || task.Status == TaskStatus.IN_PROGRESS)
            {
                TaskPanel taskPanel = CreateTaskPanel(task, inSprintContainer.gameObject.transform);
                taskPanel.onSelected += OnTaskPanelSelected;
                taskPanelCache.Add(task, taskPanel);
                task.onAssigneeChanged += UpdateSelectedTaskPanel;
            }
        }
    }

    void ClearBoard()
    {
        foreach (KeyValuePair<Task, TaskPanel> taskPanelPair in taskPanelCache)
        {
            Destroy(taskPanelPair.Value.gameObject);
        }
        taskPanelCache = null;
    }

    void UpdateSelectedTaskPanel()
    {
        Task task = taskDetailsPanel.Task;
        taskDetailsPanel.Hide(); // TODO: Move instead of hide.
        TaskPanel taskPanel = taskPanelCache[task];
        if(task.Assignee)
        {
            // Move to In Sprint if task assigned.
            inSprintContainer.Add(taskPanel);
            task.Status = TaskStatus.TO_DO;
        }
        else
        {
            // Move to backlog if no assignee.
            backlogContainer.Add(taskPanel);
            task.Status = TaskStatus.BACKLOG;
        }
        ValidateSprintReadiness();
    }

    TaskPanel CreateTaskPanel(Task task, Transform parent)
    {
        taskPanelPrefab.GetComponent<TaskPanel>().Task = task;
        TaskPanel taskPanel = Instantiate(taskPanelPrefab, parent).GetComponent<TaskPanel>();
        return taskPanel;
    }

    void ValidateSprintReadiness()
    {
        // Disable 'Begin Sprint' when no tasks are in sprint.
        sprintDetailsPanel.UpdateButtonInteraction(inSprintContainer.Contains<TaskPanel>());
    }
}
