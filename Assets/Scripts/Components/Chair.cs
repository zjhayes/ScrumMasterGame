using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField]
    private Transform seat;
    [SerializeField]
    private Transform exitToPosition;

    ICharacterController occupant;

    public void Sit(ICharacterController newOccupant)
    {
        occupant = newOccupant;

        // Disable character physics.
        occupant.EnablePhysics(false);

        // Move to seat.
        occupant.transform.SetPositionAndRotation(seat.position, seat.rotation);
    }

    public bool TrySit(ICharacterController newOccupant)
    {
        if(occupant == null)
        {
            Sit(newOccupant);
            return true;
        }
        else
        {
            return false;
        }
    }

    public ICharacterController Stand()
    {
        if (occupant == null) { return null; }

        // Enable character physics.
        occupant.EnablePhysics(true);

        // Move to exit location.
        occupant.transform.position = exitToPosition.transform.position;
        return occupant;
    }

    public bool TryStand(out ICharacterController occupant)
    {
        occupant = Stand();
        return occupant != null;
    }

    public ICharacterController Occupant { get; }

    public bool Occupied
    {
        get { return occupant != null; }
    }
}
