using UnityEngine;

public class InteractionState : CharacterState
{
    private ICharacterController character;

    public override void Handle(ICharacterController controller)
    {
        character = controller;
        base.Handle(controller);

        if (!character.CurrentInteractable)
        {
            character.Idle();
        }
        else
        {
            character.CurrentInteractable.InteractWith(character);
        }
    }

    public override string Status
    {
        get { return "Working"; }
    }
}