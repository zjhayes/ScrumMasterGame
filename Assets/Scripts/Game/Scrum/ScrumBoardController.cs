using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrumBoardController : GameBehaviour
{
    [SerializeField]
    TaskManager taskManager;
    [SerializeField]
    GameObject cartridgePrefab;
    [SerializeField]
    Container tasks;

    public void CreateCartridge(Inventory inventory)
    {
        GameObject cartridge = (GameObject) Instantiate(cartridgePrefab, transform.position, transform.rotation);
        inventory.PickUp(cartridge.GetComponent<Cartridge>());
    }

    public List<Task> ToDo
    {
        get
        {
            return taskManager.GetTasksWithStatus(TaskStatus.TO_DO);
        }
    }
}
