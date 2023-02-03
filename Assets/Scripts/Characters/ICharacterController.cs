using UnityEngine;

public interface ICharacterController : IController
{
    public void Idle();

    public void GoInteractWith(Interactable interactable);

    public void InteractWithCurrent();

    public void Frustrated();

    public void EnablePhysics(bool enable);

    public CharacterStats Stats { get; }

    public CharacterMovement Movement { get; }

    public Inventory Inventory { get; }

    public Interactable CurrentInteractable { get; }

    public StateContext<ICharacterController> StateContext { get; }

    public CharacterState State { get; }

    public Sprite Portrait { get; }

    public OverheadController OverHead { get; }
}
