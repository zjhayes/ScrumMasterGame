using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractTaskMenu : MenuController
{
    [SerializeField]
    protected TaskDetailsPanel taskDetailsPanel;
    [SerializeField]
    protected GameObject taskPanelPrefab;

    protected Dictionary<Story, TaskPanel> taskPanelCache;

    protected abstract void HandleLoadingTaskPanel(Story story);

    public override void SetUp()
    {
        // Set up sub-panels.
        taskDetailsPanel.SetUp();
        taskDetailsPanel.Hide(); // Hidden by default.
        taskDetailsPanel.onHide += OnHideTaskDetails;

        // Reload task panels when global task cache is updated.
        gameManager.Board.OnBoardUpdated += LoadTaskPanels;
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
        taskDetailsPanel.Show(taskPanel.Story);
    }

    protected void OnHideTaskDetails(MenuController taskDetails)
    {
        // Show hidden task panel.
        ShowPreviouslySelectedTaskPanel();
    }

    protected void LoadTaskPanelFor(Story story)
    {
        Debug.Log("This needs to be implemented to replace Load Task Panels");
    }

    protected void LoadTaskPanels()
    {
        // Clear existing task panels.
        ClearBoard();
        // Add tasks to board.
        taskPanelCache = new Dictionary<Story, TaskPanel>();
        foreach (Story story in gameManager.Board.Stories.Get())
        {
            HandleLoadingTaskPanel(story);
        }
    }

    protected TaskPanel CreateTaskPanel(Story story, Transform parent)
    {
        GameObject taskPanelGameObject = BehaviourBuilder.Create(taskPanelPrefab)
            .WithParent(parent)
            .WithPosition(parent.position)
            .WithRotation(parent.rotation)
            .Build<TaskPanel, IGameManager>(gameManager);
        TaskPanel taskPanel = taskPanelGameObject.GetComponent<TaskPanel>();
        taskPanel.Story = story;
        taskPanel.SetUp();
        
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
        if (taskDetailsPanel.Story != null && taskPanelCache.ContainsKey(taskDetailsPanel.Story))
        {
            // Get and show task panel for current task in task details panel.
            TaskPanel selectedTaskPanel = taskPanelCache[taskDetailsPanel.Story];
            selectedTaskPanel.Show();
        }
    }

    protected void ClearBoard()
    {
        if(taskPanelCache == null) { return; } // Nothing to clear.

        foreach (KeyValuePair<Story, TaskPanel> taskPanelPair in taskPanelCache)
        {
            // Destroy task panel.
            Destroy(taskPanelPair.Value.gameObject);
        }
        taskPanelCache = null;
    }
}
