using UnityEngine;

public class GoToInteractableState : CharacterState
{
    protected ICharacterController character;

    public override void Handle(ICharacterController controller)
    {
        character = controller;
        character.Movement.GoTo(character.TargetInteractable.Position);
        base.Handle(controller);
    }

    void Update()
    {
        if(!character.TargetInteractable)
        {
            character.Idle();
            return;
        }

        if(character.Movement.AtDestination())
        {
            character.InteractWithTarget();
        }
    }

    public override string Status 
    { 
        get { return "Walking"; }
    }
}