using UnityEngine;

public class Pickup : Interactable
{
    [SerializeField]
    private Vector3 holdRotation;

    public override void Interact(CharacterController invoker)
    {
        CharacterInventory inventory = invoker.gameObject.GetComponent<CharacterInventory>();
        AddToInventory(inventory);
        base.Interact(invoker);
    }

    public void AddToInventory(CharacterInventory target)
    {
        target?.PickUp(this);
    }

    public Vector3 HoldRotation { get { return holdRotation; } }
}