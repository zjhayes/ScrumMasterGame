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

    Dictionary<StoryStatus, Transform> statusContainerMap = new Dictionary<StoryStatus, Transform>();

    public override void SetUp()
    {
        sprintDetailsPanel.SetUp();
        // Listen to Begin Sprint button.
        sprintDetailsPanel.onBeginSprint += OnBeginSprintPressed;

        //ValidateSprintReadiness();
        statusContainerMap.Add(StoryStatus.BACKLOG, backlogContainer.gameObject.transform);
        statusContainerMap.Add(StoryStatus.TO_DO, inSprintContainer.gameObject.transform);
        statusContainerMap.Add(StoryStatus.IN_PROGRESS, inSprintContainer.gameObject.transform);
        base.SetUp();
    }

    private void OnBeginSprintPressed()
    {
        gameManager.Sprint.BeginSprint();
    }

    protected override void HandleLoadingTaskPanel(Story story)
    {
        if (statusContainerMap.TryGetValue(story.Status, out Transform containerLocation))
        {
            TaskPanel taskPanel = CreateTaskPanel(story, containerLocation);
            taskPanel.onSelected += OnTaskPanelSelected; // Listen to task clicked, show details on click.
            taskPanelCache.Add(story, taskPanel);
            taskPanel.onUpdated += UpdateTaskPanel;
        }
    }

    private void UpdateTaskPanel(TaskPanel taskPanel)
    {
        if(taskPanel.Story.Assignee != null)
        {
            // Move to In Sprint if task assigned.
            inSprintContainer.Add(taskPanel);
            taskPanel.Story.Status = StoryStatus.TO_DO;
        }
        else
        {
            // Move to backlog if no assignee.
            backlogContainer.Add(taskPanel);
            taskPanel.Story.Status = StoryStatus.BACKLOG;
        }

        // Move Task Details Panel if associated with current task.
        if(taskPanel.Story == taskDetailsPanel.Story)
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
