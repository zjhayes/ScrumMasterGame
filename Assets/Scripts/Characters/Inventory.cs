using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Transform pickupPosition;

    private Pickup currentPickup;

    public void PickUp(Pickup pickup)
    {
        TryDrop(out _); // Swap pickups if one already carried.

        // Move pickup to inventory.
        currentPickup = pickup;
        currentPickup.Move(pickupPosition, false);
        currentPickup.transform.SetParent(pickupPosition.transform);
        currentPickup.onParentChanged += OnRemoved; // Listen for pickup being removed.
    }

    public bool TryDrop(out Pickup drop)
    {
        drop = currentPickup;
        // Try to drop current pickup, if any.
        if (drop != null)
        {
            drop.ClaimedBy = null;
            drop.EnablePhysics(true);
            OnRemoved();
            return true;
        }
        else
        {
            return false; // Nothing to drop.
        }
    }

    public bool TryGetPickup<T>(out T outPickup) where T : Pickup
    {
        outPickup = currentPickup as T;

        if (outPickup != null)
        {
            return true;
        }
        else
        { 
            return false;
        }
    }

    public bool HasPickup()
    {
        return currentPickup != null;
    }

    public Pickup Pickup
    {
        get { return currentPickup; }
    }

    private void OnRemoved()
    {
        currentPickup.onParentChanged -= OnRemoved;
        currentPickup = null;
    }
}
