using UnityEngine;

public class Pickup : Interactable
{
    [SerializeField]
    private Vector3 holdRotation;

    public override void InteractWith(CharacterController character)
    {
        Debug.Log("Pick up");
        AddToInventory(character.Inventory);
        base.InteractWith(character);
    }

    public void AddToInventory(Inventory inventory)
    {
        inventory?.PickUp(this);
    }

    public Vector3 HoldRotation { get { return holdRotation; } }
}