

public class InteractionState : CharacterState
{
    public InteractionState(ICharacterController character, IGameManager gameManager) : base(character, gameManager) {}

    public override void Enter()
    {
        // Character tries interacting with its current target interactable.
        if (CharacterCanInteract(character.TargetInteractable))
        {
            character.TargetInteractable.InteractWith(character);
        }
        else
        {
            character.Frustrated();
        }
        base.Enter();
    }

    private bool CharacterCanInteract(Interactable interactable)
    {
        // Return true if character has interactable, and it is not claimed by another character.
        return interactable != null && !interactable.GetComponentInParent<Inventory>();
    }

    public override string Status
    {
        get { return "Working"; }
    }
}