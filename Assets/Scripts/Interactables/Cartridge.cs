using UnityEngine;

public class Cartridge : Pickup
{
    [SerializeField]
    Task task;

    public override void InteractWith(ICharacterController character)
    {
        base.InteractWith(character);
        character.FindSomethingToDo();
    }

    public override int CalculatePriorityFor(ICharacterController character)
    {
        if(task.Assignee == character && this.ClaimedBy == null)
        {
            return PriorityScoreConstants.PICK_UP_ASSIGNED_CARTRIDGE;
        }
        // TODO: Return less if not carried.
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
