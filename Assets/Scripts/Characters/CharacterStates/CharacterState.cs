using UnityEngine;

public abstract class CharacterState : GameBehaviour, ICharacterState
{
    public abstract string Status { get; }

    public virtual void Handle(ICharacterController controller)
    {
        this.enabled = true;
    }

    public virtual void Exit()
    {
        this.enabled = false;
    }
}
