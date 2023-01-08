using UnityEngine;

public class Pickup : Interactable, IContainable
{
    [SerializeField]
    private Vector3 holdRotation;

    public override void InteractWith(CharacterController character)
    {
        AddToInventory(character.Inventory);
        base.InteractWith(character);
    }

    public void AddToInventory(Inventory inventory)
    {
        inventory.PickUp(this);
    }

    public void EnablePhysics(bool enable)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = enable;
        gameObject.GetComponent<Rigidbody>().isKinematic = !enable;
        gameObject.GetComponent<Collider>().enabled = enable;
    }

    public Vector3 ContainerRotation { get { return holdRotation; } }
}