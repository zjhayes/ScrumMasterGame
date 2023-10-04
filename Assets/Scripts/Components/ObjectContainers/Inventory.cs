using UnityEngine;

public class Inventory : Socket
{
    public bool TryPickUp(Pickup pickup)
    {
        TryDrop(out _); // Swap pickups if one already carried.

        if(TryPut(pickup))
        {
            pickup.EnablePhysics(false);
            return true; // It was picked up.
        }
        else
        {
            Debug.LogFormat("Character was unable to pick up {0}.", pickup);
            return false;
        }
    }

    public bool TryDrop(out Pickup drop)
    {
        // Try to drop current pickup, if any.
        if (TryGet(out drop))
        {
            drop.EnablePhysics(true);
            drop.transform.SetParent(null);
            return true;
        }
        else
        {
            return false; // Nothing to drop.
        }
    }
}
