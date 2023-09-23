using UnityEngine;

public class Inventory : PickupContainer
{
    public bool TryPickUp(Pickup pickup)
    {
        TryDrop(out _); // Swap pickups if one already carried.

        if(!TryPutPickup(pickup))
        {
            Debug.LogFormat("Character was unable to pick up {0}.", pickup);
            return false;
        }
        else
        {
            return true; // it was picked up.
        }
    }

    public bool TryDrop(out Pickup drop)
    {
        // Try to drop current pickup, if any.
        if (TryGetPickup(out drop))
        {
            drop.EnablePhysics(true);
            return true;
        }
        else
        {
            return false; // Nothing to drop.
        }
    }
}
