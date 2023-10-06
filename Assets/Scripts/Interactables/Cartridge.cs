
public class Cartridge : Pickup
{
    private Story story;

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
            // TODO: Update cartridge appearance based on task.
        }
    }

    protected override void OnPickUpSuccess()
    {
        base.OnPickUpSuccess();
        if (story.Outcome.StartTime <= 0f) // TODO: Handle start/end time elsewhere.
        {
            story.Outcome.StartTime = gameManager.Sprint.Clock.CurrentTime;
        }
    }

    protected override void OnDisable()
    {
        story.Outcome.EndTime = gameManager.Sprint.Clock.CurrentTime;
        base.OnDisable();
    }
}
