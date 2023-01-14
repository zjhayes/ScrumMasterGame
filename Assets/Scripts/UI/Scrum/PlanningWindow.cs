using UnityEngine;

public class PlanningWindow : MonoBehaviour
{
    [SerializeField]
    Container backlogContainer;
    [SerializeField]
    Container inSprintContainer;
    [SerializeField]
    TaskDetailsPanel taskDetails;
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
        taskDetails.Show(task);
        sprintDetailsPanel.Hide();
    }
}
