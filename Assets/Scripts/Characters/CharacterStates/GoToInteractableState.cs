using UnityEngine;

public class GoToInteractableState : CharacterState
{
    protected CharacterController character;

    public override void Handle(CharacterController controller)
    {
        character = controller;
        character.Movement.GoTo(character.CurrentInteractable.Position);
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