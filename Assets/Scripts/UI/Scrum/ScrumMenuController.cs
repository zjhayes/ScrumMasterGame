using UnityEngine;

public class ScrumMenuController : AbstractTaskMenu
{
    [SerializeField]
    Container toDoContainer;
    [SerializeField]
    Container inProgressContainer;
    [SerializeField]
    Container doneContainer;

    protected override void HandleLoadingTaskPanel(Task task)
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
