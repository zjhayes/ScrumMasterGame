using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private Vector3 holdRotation;

    public void Interact(CharacterController invoker)
    {
        Inventory inventory = invoker.gameObject.GetComponent<Inventory>();
        AddToInventory(inventory);
    }

    public void AddToInventory(Inventory target)
    {
        target?.PickUp(this);
    }

    public Vector3 HoldRotation { get { return holdRotation; } }
}