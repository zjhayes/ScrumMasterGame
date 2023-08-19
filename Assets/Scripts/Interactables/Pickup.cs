using UnityEngine;

public abstract class Pickup : Interactable, IContainable
{
    public override void InteractWith(ICharacterController character)
    {
        character.Inventory.TryPickUp(this);
        base.InteractWith(character);
    }

    public override bool CanInteract(ICharacterController character)
    {
        // Can't pick up if already in an inventory.
        return !gameObject.GetComponentInParent<Inventory>();
    }

    public void EnablePhysics(bool enable = true)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = enable;
        gameObject.GetComponent<Rigidbody>().isKinematic = !enable;
    }
}