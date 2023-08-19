using UnityEngine;

public class InteractionState : CharacterState
{
    ICharacterController character;

    public override void Handle(ICharacterController controller)
    {
        character = controller;
        base.Handle(controller);

        if(CharacterCanInteract(character.TargetInteractable))
        {
            character.TargetInteractable.InteractWith(character);
        }
        else
        {
            character.Frustrated();
        }
    }

    private bool CharacterCanInteract(Interactable interactable)
    {
        // Return true if character has interactable, and it is not claimed by another character.
        return interactable != null && !interactable.GetComponentInParent<Inventory>();
        //return interactable != null && (interactable.ClaimedBy == null || interactable.ClaimedBy == character);
    }

    public override string Status
    {
        get { return "Working"; }
    }
}