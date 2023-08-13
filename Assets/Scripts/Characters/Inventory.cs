using UnityEngine;

public class Inventory : PickupContainer
{
    public void PickUp(Pickup pickup)
    {
        TryDrop(out _); // Swap pickups if one already carried.

        if(!TryPutPickup(pickup))
        {
            Debug.LogFormat("Character was unable to pick up {0}.", pickup);
        } // else it was picked up.
    }

    public bool TryDrop(out Pickup drop)
    {
        // Try to drop current pickup, if any.
        if (TryGetPickup(out drop))
        {
            drop.ClaimedBy = null;
            drop.EnablePhysics(true);
            return true;
        }
        else
        {
            return false; // Nothing to drop.
        }
    }
}
