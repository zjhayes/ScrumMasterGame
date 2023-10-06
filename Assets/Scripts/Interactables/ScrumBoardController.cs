
public class ScrumBoardController : Interactable
{
    public override void InteractWith(ICharacterController character)
    {
        if(gameManager.Board.Stories.AssignedTo(character).WithStatus(StoryStatus.TO_DO).TryGetFirst(out Story story))
        {
            // Character takes assigned task.
            TakeStoryCartridge(story, character);
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

    public void InstantiateCartridge(Story story, ICharacterController character)
    {
        // Get a cartridge for this task, give it to character.
        Cartridge cartridge = gameManager.ObjectPool.TakeOrCreateCartridge(character.Inventory.transform, story);
        cartridge.InteractWith(character);
    }

    public override int CalculatePriorityFor(ICharacterController character)
    {
        // Advertise to characters with assigned tasks on the board.
        if (gameManager.Board.Stories.AssignedTo(character).WithStatus(StoryStatus.TO_DO).TryGetFirst(out _))
        {
            return PriorityScore.TAKE_TASK_FROM_BOARD;
        }
        else
        {
            return PriorityScore.NO_SCORE;
        }
    }

    // Create cartridge, moving task to in progress.
    private void TakeStoryCartridge(Story story, ICharacterController character)
    {
        InstantiateCartridge(story, character);
        story.Status = StoryStatus.IN_PROGRESS;
    }
}
