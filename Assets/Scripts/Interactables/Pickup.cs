using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField]
    private Vector3 holdRotation;

    protected void PickUp(CharacterInventory target)
    {
        target?.PickUp(this);
    }

    public Vector3 HoldRotation { get { return holdRotation; } }
}
