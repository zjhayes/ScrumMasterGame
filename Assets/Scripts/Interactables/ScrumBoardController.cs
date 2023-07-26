using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrumBoardController : Interactable
{
    [SerializeField]
    GameObject cartridgePrefab;

    public override void InteractWith(ICharacterController character)
    {
        Task task = gameManager.Board.GetFirstTaskWithStatusAndAssignee(character, TaskStatus.TO_DO);
        if(task != null)
        {
            // Character takes assigned task.
            TakeTask(task, character);
        }
        else
        {
            // If no assigned task, character frustrated.
            character.Frustrated();
        }

        base.InteractWith(character);
    }

    // Create cartridge, moving task to in progress.
    void TakeTask(Task task, ICharacterController character)
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

    public override int CalculatePriorityFor(ICharacterController character)
    {
        // Advertise to characters with assigned tasks on the board.
        if(gameManager.Board.GetFirstTaskWithStatusAndAssignee(character, TaskStatus.TO_DO) != null)
        {
            return PriorityScoreConstants.TAKE_TASK_FROM_BOARD;
        }
        else
        {
            return PriorityScoreConstants.NO_SCORE;
        }
    }
}
