using System.Collections.Generic;
using UnityEngine;

public class ScrumMenuController : AbstractTaskMenu
{
    [SerializeField]
    private Container toDoContainer;
    [SerializeField]
    private Container inProgressContainer;
    [SerializeField]
    private Container doneContainer;

    private Dictionary<StoryStatus, Container> statusContainerMap;

    public override void SetUp()
    {
        // Initialize status/swimlane relational map.
        statusContainerMap = new()
        {
            { StoryStatus.TO_DO, toDoContainer },
            { StoryStatus.DONE, doneContainer },
            { StoryStatus.IN_PROGRESS, inProgressContainer }
        };
        base.SetUp();
    }

    protected override void HandleLoadingStoryPanel(Story story)
    {
        // Create story panel in swimlane pertaining to its status.
        if(statusContainerMap.TryGetValue(story.Status, out Container swimlane))
        {
            StoryPanel storyPanel = CreateTaskPanel(story, swimlane.transform);
            storyPanel.OnSelected += OnStoryPanelSelected; // Listen to task clicked, show details on click.
            storyPanelCache.Add(story, storyPanel);
            storyPanel.OnStatusUpdated += MoveStoryOnStatusChanged; // Move task across board when task status is updated.
        }
    }

    private void MoveStoryOnStatusChanged(StoryPanel storyPanel)
    {
        // Move story to swimlane pertaining to its status.
        if (statusContainerMap.TryGetValue(storyPanel.Story.Status, out Container swimlane))
        {
            swimlane.Add(storyPanel);
        }
    }
}
