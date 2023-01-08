using UnityEngine;

public abstract class CharacterState : MonoBehaviour, IState<CharacterController>
{
    public abstract string Status { get; }

    public abstract void Handle(CharacterController controller);

    public void Destroy()
    {
        Destroy(this);
    }
}
