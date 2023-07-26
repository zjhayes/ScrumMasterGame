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

    Dictionary<TaskStatus, Transform> statusContainerMap = new Dictionary<TaskStatus, Transform>();

    public override void SetUp()
    {
        statusContainerMap.Add(TaskStatus.TO_DO, toDoContainer.gameObject.transform);
        statusContainerMap.Add(TaskStatus.DONE, doneContainer.gameObject.transform);
        statusContainerMap.Add(TaskStatus.IN_PROGRESS, inProgressContainer.transform);
        base.SetUp();
    }

    protected override void HandleLoadingTaskPanel(Task task)
    {
        if(statusContainerMap.TryGetValue(task.Status, out Transform containerLocation))
        {
            TaskPanel taskPanel = CreateTaskPanel(task, containerLocation);
            taskPanel.onSelected += OnTaskPanelSelected; // Listen to task clicked, show details on click.
            taskPanelCache.Add(task, taskPanel);
            task.onStatusChanged += LoadTaskPanels; // Move task across board when task status is updated.
        }
    }
}
