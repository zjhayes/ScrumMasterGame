using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanningWindow : MonoBehaviour
{
    [SerializeField]
    Task selectedTask;
    [SerializeField]
    TaskDetailsPanel taskDetails;

    void Start()
    {
        taskDetails.UpdateDetails(selectedTask);
    }
}
