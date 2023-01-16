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
    [SerializeField]
    CameraController camera;

    Dictionary<Task, TaskPanel> taskPanelCache;

    public override void SetUp()
    {
        // Set up panels.
        taskDetailsPanel.SetUp();
        LoadTaskPanels();

        // Set controls for displaying window.
        PlayerControls.Instance.onShowBoard += ToggleBoard;
    }

    public override void Show()
    {
        camera.SwitchToBoardCamera();
        base.Show();
    }

    public override void Hide()
    {
        if (taskDetailsPanel.IsShowing)
        {
            taskDetailsPanel.Hide();
        }
        camera.SwitchToOverworldCamera();
        base.Hide();
    }

    public override void Escape()
    {
        // Don't close this window if sub-window open.
        if (taskDetailsPanel.IsShowing)
        {
            taskDetailsPanel.Escape();
        }
        else
        {
            base.Escape();
        }
    }

    void ToggleBoard()
    {
        if(IsShowing)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    void LoadTaskPanels()
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

    void OnTaskPanelSelected(TaskPanel taskPanel)
    {
        taskDetailsPanel.Task = taskPanel.Task;
        taskDetailsPanel.Show();
    }

}
