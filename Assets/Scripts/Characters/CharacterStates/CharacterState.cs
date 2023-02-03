using UnityEngine;

public abstract class CharacterState : MonoBehaviour, ICharacterState
{
    public abstract string Status { get; }

    public virtual void Handle(ICharacterController controller)
    {
        this.enabled = true;
    }

    public virtual void Destroy()
    {
        this.enabled = false;
    }
}
