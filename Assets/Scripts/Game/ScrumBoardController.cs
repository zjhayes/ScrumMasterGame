using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrumBoardController : MonoBehaviour
{
    [SerializeField]
    private GameObject cartridgePrefab;
    [SerializeField]
    private Container tasks;

    public void CreateCartridge(Inventory inventory)
    {
        GameObject cartridge = (GameObject) Instantiate(cartridgePrefab, transform.position, transform.rotation);
        inventory.PickUp(cartridge.GetComponent<Cartridge>());
    }

    public List<Task> ToDo
    {
        get
        {
            return TaskManager.Instance.GetTasksWithStatus(TaskStatus.TO_DO);
        }
    }
}
