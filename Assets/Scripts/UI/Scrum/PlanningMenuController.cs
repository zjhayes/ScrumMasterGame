using System.Collections.Generic;
using UnityEngine;

public class PlanningMenuController : AbstractTaskMenu
{
    [SerializeField]
    private SprintDetailsPanel sprintDetailsPanel;
    [SerializeField]
    private Container backlogContainer;
    [SerializeField]
    private Container inSprintContainer;

    private Dictionary<StoryStatus, Container> statusContainerMap;

    public override void SetUp()
    {
        sprintDetailsPanel.SetUp();

        // Listen to Begin Sprint button.
        sprintDetailsPanel.onBeginSprint += OnBeginSprintPressed;

        // Initialize status/swimlane relational map.
        statusContainerMap = new()
        {
            { StoryStatus.BACKLOG, backlogContainer },
            { StoryStatus.TO_DO, inSprintContainer },
            { StoryStatus.IN_PROGRESS, inSprintContainer }
        };
        base.SetUp();
    }

    public override void Show()
    {
        base.Show();
        ValidateSprintReadiness();
    }

    private void OnBeginSprintPressed()
    {
        gameManager.Sprint.BeginSprint();
    }

    protected override void HandleLoadingStoryPanel(Story story)
    {
        if (statusContainerMap.TryGetValue(story.Status, out Container container))
        {
            StoryPanel storyPanel = CreateTaskPanel(story, container.transform);
            storyPanel.OnSelected += OnStoryPanelSelected; // Listen to task clicked, show details on click.
            storyPanelCache.Add(story, storyPanel);
            storyPanel.OnAssigneeUpdated += UpdateStoryStatusOnAssigneeChanged;
            storyPanel.OnStatusUpdated += MoveStoryOnStatusChanged;
        }
    }

    private void UpdateStoryStatusOnAssigneeChanged(StoryPanel storyPanel)
    {
        if (storyPanel.Story.Assignee != null)
        {
            // Move to In Sprint if task assigned.
            storyPanel.Story.Status = StoryStatus.TO_DO;
        }
        else
        {
            // Move to backlog if no assignee.
            storyPanel.Story.Status = StoryStatus.BACKLOG;
        }
    }

    private void MoveStoryOnStatusChanged(StoryPanel storyPanel)
    {
        if (statusContainerMap.TryGetValue(storyPanel.Story.Status, out Container containerLocation))
        {
            containerLocation.Add(storyPanel);
        }

        // Move Task Details Panel if associated with current task.
        if (storyPanel.Story == taskDetailsPanel.Story)
        {
            MoveStoryDetailsPanelToStoryPanel(storyPanel);
            taskDetailsPanel.SnapScroll();
        }
        ValidateSprintReadiness();
    }

    private void ValidateSprintReadiness()
    {
        // Disable 'Begin Sprint' when no tasks are in sprint.
        bool includeInactive = true;
        sprintDetailsPanel.UpdateButtonInteraction(inSprintContainer.Contains<StoryPanel>(includeInactive));
    }
}
