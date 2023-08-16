using UnityEngine;

public abstract class Pickup : Interactable, IContainable
{
    public override void InteractWith(ICharacterController character)
    {
        this.ClaimedBy = character;
        character.Inventory.TryPickUp(this);
        base.InteractWith(character);
    }

    public void EnablePhysics(bool enable = true)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = enable;
        gameObject.GetComponent<Rigidbody>().isKinematic = !enable;
        //gameObject.GetComponent<Collider>().enabled = enable;
    }
}