
public class ScrumBoardController : Interactable
{
    public override void InteractWith(ICharacterController character)
    {
        if(gameManager.Board.TryGetFirstTaskWithStatusAndAssignee(character, TaskStatus.TO_DO, out Task task))
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

    public override bool CanInteract(ICharacterController character)
    {
        return true; // Character can always interact with scrum board.
    }

    public void InstantiateCartridge(Task task, ICharacterController character)
    {
        // Get a cartridge for this task, give it to character.
        Cartridge cartridge = gameManager.ObjectPool.TakeOrCreateCartridge(character.Inventory.transform, task);
        cartridge.InteractWith(character);
    }

    public override int CalculatePriorityFor(ICharacterController character)
    {
        // Advertise to characters with assigned tasks on the board.
        if (gameManager.Board.TryGetFirstTaskWithStatusAndAssignee(character, TaskStatus.TO_DO, out _))
        {
            return PriorityScore.TAKE_TASK_FROM_BOARD;
        }
        else
        {
            return PriorityScore.NO_SCORE;
        }
    }

    // Create cartridge, moving task to in progress.
    private void TakeTask(Task task, ICharacterController character)
    {
        InstantiateCartridge(task, character);
        task.Status = TaskStatus.IN_PROGRESS;
    }
}
