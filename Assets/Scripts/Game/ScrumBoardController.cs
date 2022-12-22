using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrumBoardController : Interactable
{
    [SerializeField]
    private GameObject cartridgePrefab;
    [SerializeField]
    private Container toDoContainer;
    [SerializeField]
    private Container inProgressContainer;
    [SerializeField]
    private Container doneContainer;

    public override void Interact(CharacterController invoker)
    {
        base.Interact(invoker);
    }

    public void CreateCartridge(CharacterInventory inventory)
    {
        GameObject cartridge = (GameObject) Instantiate(cartridgePrefab, transform.position, transform.rotation);
        inventory.PickUp(cartridge.GetComponent<Cartridge>());
    }

    public List<Task> ToDo
    {
        get
        {
            return CollectTasks(toDoContainer);
        }
    }

    private List<Task> CollectTasks(Container container)
    {
        List<Task> tasks = new List<Task>();
        List<GameObject> taskObjects = container.Get(Tags.TASK);
        foreach (GameObject task in taskObjects)
        {
            tasks.Add(task.GetComponent<Task>());
        }
        return tasks;
    }
}
