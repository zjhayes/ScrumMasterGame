using UnityEngine;

public class TaskPanel : MonoBehaviour
{
    [SerializeField]
    private Task task;

    public Task Task
    {
        get { return task; }
        set { task = value; }
    }
}
