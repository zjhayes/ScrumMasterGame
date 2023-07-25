using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrumBoardController : Interactable
{
    [SerializeField]
    GameObject cartridgePrefab;

    // Create cartridge, moving task to in progress.
    public void TakeTask(Task task, ICharacterController character)
    {
        InstantiateCartridge(task, character);
        task.Status = TaskStatus.IN_PROGRESS;
    }

    public void InstantiateCartridge(Task task, ICharacterController character)
    {
        // Instantiate Cartridge prefab for this task.
        GameObject cartridgeObject = BehaviourFactory.Create<Cartridge, IGameManager>(cartridgePrefab, gameManager, transform.position, transform.rotation);
        Cartridge cartridge = cartridgeObject.GetComponent<Cartridge>();
        cartridge.Task = task;
        cartridge.InteractWith(character);
    }

    public override void InteractWith(ICharacterController character)
    {
        // Character takes assigned task.
        Task task = gameManager.Board.GetFirstTaskWithAssignee(character);
        if(task != null)
        {
            TakeTask(task, character);
        }
        else
        {
            // If no assigned task, character frustrated.
            character.Frustrated();
        }

        base.InteractWith(character);
    }
}
