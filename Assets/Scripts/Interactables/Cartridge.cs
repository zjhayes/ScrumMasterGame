
public class Cartridge : Pickup
{
    private Task task;

    public override void InteractWith(ICharacterController character)
    {
        base.InteractWith(character);
        character.FindSomethingToDo();
    }

    public override int CalculatePriorityFor(ICharacterController character)
    {
        if (gameObject.activeSelf && task.Assignee == character && !character.Inventory.HasPickup())
        {
            // Task is assigned to character with free hands, pick it up.
            return PriorityScoreConstants.PICK_UP_ASSIGNED_CARTRIDGE;
        }
        // TODO: Handle cartridges left by other characters.
        return PriorityScoreConstants.NO_SCORE;
    }

    public Task Task
    {
        get { return task; }
        set
        {
            task = value;
            // TODO: Update cartridge appearance based on task.
        }
    }
}
