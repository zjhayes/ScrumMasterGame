using UnityEngine;

public abstract class Pickup : Interactable, IContainable
{
    [SerializeField]
    private Vector3 holdRotation;

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

    public void EnablePhysics(bool enable)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = enable;
        gameObject.GetComponent<Rigidbody>().isKinematic = !enable;
        gameObject.GetComponent<Collider>().enabled = enable;
    }

    public void SetPositionToContainer(Container container)
    {
        gameObject.transform.position = container.gameObject.transform.position;
    }

    public void SetToHoldRotation()
    {
        gameObject.transform.localEulerAngles = holdRotation;
    }
}