using UnityEngine;

public class PlanningMenuController : AbstractTaskMenu
{
    [SerializeField]
    SprintDetailsPanel sprintDetailsPanel;
    [SerializeField]
    Container backlogContainer;
    [SerializeField]
    Container inSprintContainer;
    
    public override void SetUp()
    {
        sprintDetailsPanel.SetUp();
        // Listen to Begin Sprint button.
        sprintDetailsPanel.onBeginSprint += OnBeginSprintPressed;

        ValidateSprintReadiness();
        base.SetUp();
    }

    void OnBeginSprintPressed()
    {
        GameManager.Instance.Sprint.BeginSprint();
    }

    protected override void HandleLoadingTaskPanel(Task task)
    {
        if (task.Status == TaskStatus.BACKLOG)
        {
            TaskPanel taskPanel = CreateTaskPanel(task, backlogContainer.gameObject.transform);
            taskPanel.onSelected += OnTaskPanelSelected; // Listen to task clicked, show details on click.
            taskPanelCache.Add(task, taskPanel);
            taskPanel.onUpdated += UpdateTaskPanel;
        }
        else if (task.Status == TaskStatus.TO_DO || task.Status == TaskStatus.IN_PROGRESS)
        {
            TaskPanel taskPanel = CreateTaskPanel(task, inSprintContainer.gameObject.transform);
            taskPanel.onSelected += OnTaskPanelSelected;
            taskPanelCache.Add(task, taskPanel);
            taskPanel.onUpdated += UpdateTaskPanel;
        }
    }

    void UpdateTaskPanel(TaskPanel taskPanel)
    {
        if(taskPanel.Task.Assignee)
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

    void ValidateSprintReadiness()
    {
        // Disable 'Begin Sprint' when no tasks are in sprint.
        sprintDetailsPanel.UpdateButtonInteraction(inSprintContainer.Contains<TaskPanel>());
    }
}
