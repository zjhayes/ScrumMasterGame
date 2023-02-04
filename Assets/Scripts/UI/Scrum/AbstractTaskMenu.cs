using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class AbstractTaskMenu : MenuController
{
    [SerializeField]
    protected TaskDetailsPanel taskDetailsPanel;
    [SerializeField]
    protected GameObject taskPanelPrefab;

    protected Dictionary<Task, TaskPanel> taskPanelCache;

    protected abstract void HandleLoadingTaskPanel(Task task);

    public override void SetUp()
    {
        // Set up sub-panels.
        taskDetailsPanel.SetUp();
        taskDetailsPanel.Hide(); // Hidden by default.
        taskDetailsPanel.onHide += OnHideTaskDetails;

        base.SetUp();
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
            base.Escape();
        }
    }

    protected void OnTaskPanelSelected(TaskPanel taskPanel)
    {
        ShowPreviouslySelectedTaskPanel(); // Show previously selected task, if any.
        // Replace task panel with task details panel.
        MoveTaskDetailsPanelToTaskPanel(taskPanel);
        taskPanel.Hide();
        taskDetailsPanel.Show(taskPanel.Task);
    }

    protected void OnHideTaskDetails(MenuController taskDetails)
    {
        // Show hidden task panel.
        ShowPreviouslySelectedTaskPanel();
    }

    protected void LoadTaskPanels()
    {
        // Clear existing task panels.
        ClearBoard();
        // Add tasks to board.
        taskPanelCache = new Dictionary<Task, TaskPanel>();
        foreach (Task task in gameManager.Sprint.Board.Tasks)
        {
            HandleLoadingTaskPanel(task);
        }
    }

    protected TaskPanel CreateTaskPanel(Task task, Transform parent)
    {
        taskPanelPrefab.GetComponent<TaskPanel>().Task = task;
        TaskPanel taskPanel = Instantiate(taskPanelPrefab, parent).GetComponent<TaskPanel>();
        return taskPanel;
    }

    protected void MoveTaskDetailsPanelToTaskPanel(TaskPanel taskPanel)
    {
        taskDetailsPanel.gameObject.transform.SetParent(taskPanel.gameObject.transform.parent);
        taskDetailsPanel.gameObject.transform.SetSiblingIndex(taskPanel.gameObject.transform.GetSiblingIndex());
    }

    protected void ShowPreviouslySelectedTaskPanel()
    {
        // Check if there's a selected task, then show it.
        if (taskDetailsPanel.Task && taskPanelCache.ContainsKey(taskDetailsPanel.Task))
        {
            // Get and show task panel for current task in task details panel.
            TaskPanel selectedTaskPanel = taskPanelCache[taskDetailsPanel.Task];
            selectedTaskPanel.Show();
        }
    }

    protected void ClearBoard()
    {
        if (taskPanelCache == null)
        {
            return; // Already clear.
        }

        foreach (KeyValuePair<Task, TaskPanel> taskPanelPair in taskPanelCache)
        {
            // Destroy task panel.
            Destroy(taskPanelPair.Value.gameObject);
        }
        taskPanelCache = null;
    }
}
