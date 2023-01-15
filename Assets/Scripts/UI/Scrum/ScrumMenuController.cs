using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrumMenuController : MenuController
{
    
    [SerializeField]
    ScrumBoardController boardController;
    [SerializeField]
    VerticalLayoutGroup toDoColumn;
    [SerializeField]
    GameObject taskPanelPrefab;

    public void Interact(CharacterController invoker)
    {
        boardController.CreateCartridge(invoker.GetComponent<Inventory>());
    }

    public override void SetUp()
    {
        base.SetUp();
        // Show on Scrum Board interactions.
        //boardController.onInteract += Show;
    }

    public override void Show()
    {
        base.Show();
        Load();
    }

    private void Load()
    {
        // Add 'To Do' tasks to board.
        List<Task> toDoTasks = boardController.ToDo;
        AddTasksToCanvas(toDoTasks, toDoColumn);
    }

    private void AddTasksToCanvas(List<Task> tasks, VerticalLayoutGroup column)
    {
        foreach (Task task in tasks)
        {
            GameObject taskPanel = (GameObject) Instantiate(taskPanelPrefab, transform.position, transform.rotation);
            taskPanel.transform.SetParent(column.gameObject.transform);
            taskPanel.GetComponent<TaskPanel>().Task = task;
        }
    }
    
}
