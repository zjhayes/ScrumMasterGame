using System.Collections.Generic;
using UnityEngine;

public class PlanningMenuController : AbstractTaskMenu
{
    [SerializeField]
    SprintDetailsPanel sprintDetailsPanel;
    [SerializeField]
    Container backlogContainer;
    [SerializeField]
    Container inSprintContainer;

    Dictionary<TaskStatus, Transform> statusContainerMap = new Dictionary<TaskStatus, Transform>();

    public override void SetUp()
    {
        sprintDetailsPanel.SetUp();
        // Listen to Begin Sprint button.
        sprintDetailsPanel.onBeginSprint += OnBeginSprintPressed;

        //ValidateSprintReadiness();
        statusContainerMap.Add(TaskStatus.BACKLOG, backlogContainer.gameObject.transform);
        statusContainerMap.Add(TaskStatus.TO_DO, inSprintContainer.gameObject.transform);
        statusContainerMap.Add(TaskStatus.IN_PROGRESS, inSprintContainer.gameObject.transform);
        base.SetUp();
    }

    private void OnBeginSprintPressed()
    {
        gameManager.Sprint.BeginSprint();
    }

    protected override void HandleLoadingTaskPanel(Task task)
    {
        if (statusContainerMap.TryGetValue(task.Status, out Transform containerLocation))
        {
            TaskPanel taskPanel = CreateTaskPanel(task, containerLocation);
            taskPanel.onSelected += OnTaskPanelSelected; // Listen to task clicked, show details on click.
            taskPanelCache.Add(task, taskPanel);
            taskPanel.onUpdated += UpdateTaskPanel;
        }
    }

    private void UpdateTaskPanel(TaskPanel taskPanel)
    {
        if(taskPanel.Task.Assignee != null)
        {
            // Move to In Sprint if task assigned.
            inSprintContainer.Add(taskPanel);
            taskPanel.Task.Status = TaskStatus.TO_DO;
        }
        else
        {
            // Move to backlog if no assignee.
            backlogContainer.Add(taskPanel);
            taskPanel.Task.Status = TaskStatus.BACKLOG;
        }

        // Move Task Details Panel if associated with current task.
        if(taskPanel.Task == taskDetailsPanel.Task)
        {
            MoveTaskDetailsPanelToTaskPanel(taskPanel);
        }
        ValidateSprintReadiness();
    }

    private void ValidateSprintReadiness()
    {
        // Disable 'Begin Sprint' when no tasks are in sprint.
        bool includeInactive = true;
        sprintDetailsPanel.UpdateButtonInteraction(inSprintContainer.Contains<TaskPanel>(includeInactive));
    }

    private void OnEnable()
    {
        ValidateSprintReadiness();
    }
}
