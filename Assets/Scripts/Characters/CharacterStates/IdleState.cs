using UnityEngine;

public class IdleState : CharacterState
{
    private CharacterController character;

    public override void Handle(CharacterController _controller)
    {
        character = _controller;
    }

    public override string Status
    {
        get { return "Idle"; }
    }
}