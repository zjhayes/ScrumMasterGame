using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField]
    Transform seat;
    [SerializeField]
    Transform exitToPosition;

    public void Sit(ICharacterController occupant)
    {
        // Disable character physics.
        occupant.EnablePhysics(false);

        // Move to seat.
        occupant.transform.parent = seat;
        occupant.transform.position = seat.position;
        occupant.transform.rotation = seat.rotation;
    }

    public void Stand()
    {
        ICharacterController occupant = Occupant;
        if (occupant == null) { return; }

        // Enable character physics.
        occupant.EnablePhysics(true);

        // Move to original location.
        occupant.transform.parent = null;
        occupant.transform.position = exitToPosition.transform.position;
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
