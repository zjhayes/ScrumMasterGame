using System.Collections.Generic;
using UnityEngine;

public class ScrumMenuController : AbstractTaskMenu
{
    [SerializeField]
    Container toDoContainer;
    [SerializeField]
    Container inProgressContainer;
    [SerializeField]
    Container doneContainer;

    readonly Dictionary<StoryStatus, Transform> statusContainerMap = new();

    public override void SetUp()
    {
        statusContainerMap.Add(StoryStatus.TO_DO, toDoContainer.gameObject.transform);
        statusContainerMap.Add(StoryStatus.DONE, doneContainer.gameObject.transform);
        statusContainerMap.Add(StoryStatus.IN_PROGRESS, inProgressContainer.transform);
        base.SetUp();
    }

    protected override void HandleLoadingTaskPanel(Story task)
    {
        if(statusContainerMap.TryGetValue(task.Status, out Transform containerLocation))
        {
            TaskPanel taskPanel = CreateTaskPanel(task, containerLocation);
            taskPanel.onSelected += OnTaskPanelSelected; // Listen to task clicked, show details on click.
            taskPanelCache.Add(task, taskPanel);
            task.OnStatusChanged += LoadTaskPanelFor; // Move task across board when task status is updated.
        }
    }
}
