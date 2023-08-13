using UnityEngine;

/* Container for a single Pickup. */
public class PickupContainer : MonoBehaviour
{
    protected Pickup currentPickup;

    public delegate void OnPickupRemoved();
    public event OnPickupRemoved onPickupRemoved;

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

    public bool HasPickup()
    {
        return currentPickup != null;
    }

    public Pickup Pickup
    {
        get { return currentPickup; }
    }

    protected virtual void PickupRemoved()
    {
        currentPickup = null;
        onPickupRemoved?.Invoke();
    }

    private void OnTransformChildrenChanged()
    {
        if (transform.childCount <= 0)
        {
            PickupRemoved();
        }
    }
}
