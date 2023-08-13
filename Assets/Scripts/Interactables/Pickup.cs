using UnityEngine;

public abstract class Pickup : Interactable, IContainable
{
    public override void InteractWith(ICharacterController character)
    {
        this.ClaimedBy = character;
        AddToInventory(character.Inventory);
        base.InteractWith(character);
    }

    public void AddToInventory(Inventory inventory)
    {
        inventory.PickUp(this);
    }

    public void Move(Transform transformParent, bool enablePhysics = true)
    {
        EnablePhysics(enablePhysics);
        gameObject.transform.position = transformParent.position;
        gameObject.transform.rotation = transformParent.rotation;
        gameObject.transform.SetParent(transformParent);
    }

    public void MoveToContainer(PickupContainer container)
    {
        EnablePhysics(false);
        container.TryPutPickup(this);

    }

    public void EnablePhysics(bool enable = true)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = enable;
        gameObject.GetComponent<Rigidbody>().isKinematic = !enable;
        gameObject.GetComponent<Collider>().enabled = enable;
    }
}