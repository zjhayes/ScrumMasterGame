using UnityEngine;

public class InteractionState : CharacterState
{
    private CharacterController character;

    public override void Handle(CharacterController _controller)
    {
        character = _controller;
    }

    void Start()
    {
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