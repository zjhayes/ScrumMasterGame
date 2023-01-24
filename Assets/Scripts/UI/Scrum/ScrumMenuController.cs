using System.Collections.Generic;
using System.Linq;
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
    [SerializeField]
    GameObject taskPanelPrefab;

    Dictionary<Task, TaskPanel> taskPanelCache;

    public override void SetUp()
    {
        // Set up sub-panels.
        taskDetailsPanel.SetUp();
        taskDetailsPanel.Hide(); // Hidden by default.
        taskDetailsPanel.onHide += OnHideTaskDetails;
    }

    public override void Show()
    {
        // Add tasks to board.
        LoadTaskPanels();
        base.Show();
    }

    public override void Hide()
    {
        if (taskDetailsPanel.IsShowing)
        {
            taskDetailsPanel.Hide();
        }
        ClearBoard();
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
            base.Escape(); // Scrum Menu is not escapable.
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
        if(taskDetailsPanel.Task && taskPanelCache.ContainsKey(taskDetailsPanel.Task))
        {
            // Get and show task panel for current task in task details panel.
            TaskPanel selectedTaskPanel = taskPanelCache[taskDetailsPanel.Task];
            selectedTaskPanel.Show();
        }
    }

    void LoadTaskPanels()
    {
        ClearBoard();
        // Add tasks to board.
        taskPanelCache = new Dictionary<Task, TaskPanel>();
        foreach (Task task in TaskManager.Instance.Tasks)
        {
            if (task.Status == TaskStatus.TO_DO)
            {
                TaskPanel taskPanel = CreateTaskPanel(task, toDoContainer.gameObject.transform);
                taskPanel.onSelected += OnTaskPanelSelected; // Listen to task clicked, show details on click.
                taskPanelCache.Add(task, taskPanel);
            }
            else if (task.Status == TaskStatus.IN_PROGRESS)
            {
                TaskPanel taskPanel = CreateTaskPanel(task, inProgressContainer.gameObject.transform);
                taskPanel.onSelected += OnTaskPanelSelected;
                taskPanelCache.Add(task, taskPanel);
            }
            else if (task.Status == TaskStatus.DONE)
            {
                TaskPanel taskPanel = CreateTaskPanel(task, doneContainer.gameObject.transform);
                taskPanel.onSelected += OnTaskPanelSelected;
                taskPanelCache.Add(task, taskPanel);
            }
        }
    }

    void ClearBoard()
    {
        if(taskPanelCache == null)
        {
            return; // Already clear.
        }

        foreach(KeyValuePair<Task,TaskPanel> taskPanelPair in taskPanelCache)
        {
            // Destroy task panel.
            Destroy(taskPanelPair.Value.gameObject);
        }
        taskPanelCache = null;
    }

    public TaskPanel CreateTaskPanel(Task task, Transform parent)
    {
        taskPanelPrefab.GetComponent<TaskPanel>().Task = task;
        TaskPanel taskPanel = Instantiate(taskPanelPrefab, parent).GetComponent<TaskPanel>();
        return taskPanel;
    }
}
