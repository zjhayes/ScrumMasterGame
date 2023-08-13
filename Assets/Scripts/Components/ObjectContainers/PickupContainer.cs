using UnityEngine;

/* Container for a single Pickup. */
public class PickupContainer : MonoBehaviour
{
    protected Pickup currentPickup;

    public delegate void OnRemoved();
    public event OnRemoved onRemoved;

    public bool TryPutPickup(Pickup pickup)
    {
        if(currentPickup == null)
        {
            currentPickup = pickup;
            pickup.EnablePhysics(false);
            pickup.gameObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
            pickup.gameObject.transform.SetParent(transform);
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
        currentPickup = null;
        onRemoved?.Invoke();
    }

    private void OnTransformChildrenChanged()
    {
        if (transform.childCount <= 0)
        {
            PickupRemoved();
        }
    }
}
