using UnityEngine;

public class IdleState : CharacterState
{
    private ICharacterController character;

    public override void Handle(ICharacterController controller)
    {
        character = controller;
        base.Handle(controller);
    }

    public override string Status
    {
        get { return "Idle"; }
    }
}