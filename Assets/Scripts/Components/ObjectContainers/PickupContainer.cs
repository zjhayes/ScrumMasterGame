using UnityEngine;

/* Container for a single Pickup. */
public class PickupContainer : MonoBehaviour
{
    protected Pickup currentPickup;

    public event Events.InteractableEvent<Pickup> OnRemoved;

    public bool TryPutPickup(Pickup pickup)
    {
        if(currentPickup == null)
        {
            currentPickup = pickup;
            pickup.EnablePhysics(false);
            pickup.transform.SetPositionAndRotation(transform.position, transform.rotation);
            pickup.transform.SetParent(transform);
            return true;
        }
        else
        {
            return false; // Already has pickup.
        }
    }
     
    public bool TryGetPickup<T>(out T outPickup) where T : Pickup
    {
        outPickup = currentPickup as T;
        return outPickup != null;
    }

    public bool HasPickup<T>() where T : Pickup
    {
        return currentPickup != null && currentPickup is T;
    }

    public bool HasPickup()
    {
        return HasPickup<Pickup>();
    }

    protected virtual void PickupRemoved()
    {
        OnRemoved?.Invoke(currentPickup);
        currentPickup = null;
    }

    // Listen for when pickup is removed as child of container.
    private void OnTransformChildrenChanged()
    {
        if (transform.childCount <= 0)
        {
            PickupRemoved();
        }
    }
}
