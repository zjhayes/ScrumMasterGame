using UnityEngine;

public interface ICharacterController : IController
{
    public void Idle();

    public void GoInteractWith(Interactable interactable);

    public void FindSomethingToDo();

    public void InteractWithTarget();

    public void Frustrated();
    public void Deselect();
    public void EnablePhysics(bool enable);
    public void ClearTargetInteractable();

    public CharacterStats Stats { get; }

    public CharacterMovement Movement { get; }

    public Inventory Inventory { get; }

    public Interactable TargetInteractable { get; }

    public CharacterContext Context { get; }

    public CharacterState State { get; }

    public Sprite Portrait { get; }
}
