using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Container inventory;

    private void Awake()
    {
        if(inventory == null)
        {
            Debug.Log(string.Format("Character {0} does not have an inventory container.", gameObject));
        }
    }

    public void PickUp(Pickup pickup)
    {
        TryDrop(out _); // Swap pickups if one already carried.

        // Move pickup to inventory.
        inventory.Add(pickup);
        pickup.Move(inventory.gameObject.transform.position, false);
    }

    public bool TryDrop(out Pickup drop)
    {
        // Try to drop current pickup, if any.
        if (TryGetPickup(out drop))
        {
            inventory.Remove(drop);
            drop.ClaimedBy = null;
            drop.EnablePhysics(true);
            return true;
        }
        else
        {
            return false; // Nothing to drop.
        }
    }

    public bool HasPickup()
    {
        return !inventory.IsEmpty;
    }

    // Returns true if inventory contains type of pickup.
    public bool TryGetPickup<T>(out T pickup) where T : Pickup
    {
        return inventory.TryGetFirst(out pickup);
    }
}
