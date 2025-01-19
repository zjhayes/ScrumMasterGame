
public class Cartridge : Pickup
{
    private Story story;

    public event Events.GameEvent OnStoryUpdated;

    public override void InteractWith(ICharacterController character)
    {
        base.InteractWith(character);
        character.FindSomethingToDo(); // Go work on this... or get distracted.
    }

    public override int CalculatePriorityFor(ICharacterController character)
    {
        if (gameObject.activeSelf && story.Assignee == character && !character.Inventory.Has<Cartridge>())
        {
            // Task is assigned to character with free hands, pick it up.
            return PriorityScore.PICK_UP_ASSIGNED_CARTRIDGE;
        }
        // TODO: Handle cartridges left by other characters.
        return PriorityScore.NO_SCORE;
    }

    public Story Story
    {
        get { return story; }
        set
        {
            story = value;
            story.OnAssigneeChanged += CacheOnUnassigned;
            OnStoryUpdated.Invoke();
        }
    }

    private void CacheOnUnassigned(ICharacterController character)
    {
        if (character == null)
        {
            // Cache cartridge object.
            gameManager.ObjectPool.PoolCartridge(this);
        }
    }
}
