using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField]
    Transform seat;
    [SerializeField]
    Transform exitToPosition;

    public void Sit(CharacterController occupant)
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
        CharacterController occupant = Occupant;
        if (!occupant) { return; }

        // Enable character physics.
        occupant.EnablePhysics(true);

        // Move to original location.
        occupant.transform.parent = null;
        occupant.transform.position = exitToPosition.transform.position;
    }

    public CharacterController Occupant
    {
        get 
        {
            return seat.GetComponentInChildren(typeof(CharacterController)) as CharacterController;
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
