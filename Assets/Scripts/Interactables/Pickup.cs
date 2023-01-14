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

    public void OnContained(Container container)
    {
        EnablePhysics(false);
        SetPositionToContainer(container);
        SetToHoldRotation();
    }

    public void OnRemoved(Container container)
    {
        EnablePhysics(true);
    }

    private void EnablePhysics(bool enable)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = enable;
        gameObject.GetComponent<Rigidbody>().isKinematic = !enable;
        gameObject.GetComponent<Collider>().enabled = enable;
    }

    private void SetPositionToContainer(Container container)
    {
        gameObject.transform.position = container.gameObject.transform.position;
    }

    private void SetToHoldRotation()
    {
        gameObject.transform.localEulerAngles = holdRotation;
    }
}