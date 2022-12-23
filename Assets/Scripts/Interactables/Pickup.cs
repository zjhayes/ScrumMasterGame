using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private Vector3 holdRotation;

    public void Interact(CharacterController invoker)
    {
        CharacterInventory inventory = invoker.gameObject.GetComponent<CharacterInventory>();
        AddToInventory(inventory);
    }

    public void AddToInventory(CharacterInventory target)
    {
        target?.PickUp(this);
    }

    public Vector3 HoldRotation { get { return holdRotation; } }
}