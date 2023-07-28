using UnityEngine;

public class InteractionState : CharacterState
{
    ICharacterController character;
    Interactable interactable;

    public override void Handle(ICharacterController controller)
    {
        character = controller;
        interactable = character.TargetInteractable;
        base.Handle(controller);

        if(CharacterCanInteract())
        {
            interactable.InteractWith(character);
        }
        else
        {
            character.Frustrated();
        }
    }

    bool CharacterCanInteract()
    {
        // Return true if character has interactable, and it is not claimed by another character.
        return interactable != null && (interactable.ClaimedBy == null || interactable.ClaimedBy == character);
    }

    public override string Status
    {
        get { return "Working"; }
    }
}