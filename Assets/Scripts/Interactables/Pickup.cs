using UnityEngine;

public abstract class Pickup : Interactable, IContainable
{
    public delegate void OnParentChanged();
    public event OnParentChanged onParentChanged;

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

    public void EnablePhysics(bool enable = true)
    {
        gameObject.GetComponent<Rigidbody>().useGravity = enable;
        gameObject.GetComponent<Rigidbody>().isKinematic = !enable;
        gameObject.GetComponent<Collider>().enabled = enable;
    }

    private void OnTransformParentChanged()
    {
        onParentChanged?.Invoke();
    }
}