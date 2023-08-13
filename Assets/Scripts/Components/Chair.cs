using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField]
    private Transform seat;
    [SerializeField]
    private Transform exitToPosition;

    public void Sit(ICharacterController occupant)
    {
        // Disable character physics.
        occupant.EnablePhysics(false);

        // Move to seat.
        occupant.transform.parent = seat;
        occupant.transform.SetPositionAndRotation(seat.position, seat.rotation);
    }

    public ICharacterController Stand()
    {
        ICharacterController occupant = Occupant;
        if (occupant == null) { return null; }

        // Enable character physics.
        occupant.EnablePhysics(true);

        // Move to original location.
        occupant.transform.parent = null;
        occupant.transform.position = exitToPosition.transform.position;
        return occupant;
    }

    public bool TryStand(out ICharacterController occupant)
    {
        occupant = Stand();
        return occupant != null;
    }

    public ICharacterController Occupant
    {
        get 
        {
            return seat.GetComponentInChildren(typeof(ICharacterController)) as ICharacterController;
        }
    }

    public bool Occupied
    {
        get 
        {
            if (seat.transform.childCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
