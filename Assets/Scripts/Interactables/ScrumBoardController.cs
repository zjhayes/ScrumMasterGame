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
        //GameObject cartridgeObject = (GameObject)Instantiate(cartridgePrefab, transform.position, transform.rotation);
        GameObject cartridgeObject = BehaviourFactory.Create<Cartridge,IGameManager>(cartridgePrefab, gameManager, transform.position, transform.rotation);
        Cartridge cartridge = cartridgeObject.GetComponent<Cartridge>();
        cartridge.Task = task;
        cartridge.InteractWith(character);
        task.Status = TaskStatus.IN_PROGRESS;
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
