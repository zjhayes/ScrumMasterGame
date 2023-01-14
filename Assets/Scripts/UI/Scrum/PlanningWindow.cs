using UnityEngine;

public class PlanningWindow : MonoBehaviour
{
    [SerializeField]
    Container backlogContainer;
    [SerializeField]
    Container inSprintContainer;
    [SerializeField]
    TaskDetailsPanel taskDetailsPanel;
    [SerializeField]
    SprintDetailsPanel sprintDetailsPanel;


    void Start()
    {
        foreach(Task task in TaskManager.Instance.Tasks)
        {
            if (task.Status == TaskStatus.BACKLOG)
            {
                TaskPanel taskPanel = UIManager.Instance.CreateTaskPanel(task, backlogContainer.gameObject.transform);
            }
        }
    }

    public void ShowTaskDetails(Task task)
    {
        taskDetailsPanel.Task = task;

        if(taskDetailsPanel.IsShowing)
        {
            taskDetailsPanel.UpdateDetails();
            taskDetailsPanel.UpdateAssignee();
        }
        else
        {
            taskDetailsPanel.Show();
        }

        if(sprintDetailsPanel.IsShowing)
        {
            sprintDetailsPanel.Hide();
        }
    }



}
