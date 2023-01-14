using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanningWindow : MonoBehaviour
{
    [SerializeField]
    Container backlogContainer;
    [SerializeField]
    Container inSprintContainer;
    [SerializeField]
    TaskDetailsPanel taskDetails;

    void Start()
    {
        foreach(Task task in TaskManager.Instance.Tasks)
        {
            if (task.Status == TaskStatus.BACKLOG)
            {
                TaskPanel taskPanel = UIManager.Instance.CreateTaskPanel(task, backlogContainer.gameObject.transform);
            }
        }

        //taskDetails.UpdateDetails(selectedTask);
    }
}
