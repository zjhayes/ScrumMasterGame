using UnityEngine;

public class GoToInteractableState : CharacterState
{
    protected ICharacterController character;

    public override void Handle(ICharacterController controller)
    {
        character = controller;
        character.Movement.GoTo(character.CurrentInteractable.Position);
        base.Handle(controller);
    }

    void Update()
    {
        if(!character.CurrentInteractable)
        {
            character.Idle();
            return;
        }

        if(character.Movement.AtDestination())
        {
            character.InteractWithCurrent();
        }
    }

    public override string Status 
    { 
        get { return "Walking"; }
    }
}