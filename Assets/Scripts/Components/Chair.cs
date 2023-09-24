using UnityEngine;

public class Chair : MonoBehaviour
{
    [SerializeField]
    private CharacterSeat seat;
    [SerializeField]
    private Transform exitToPosition;

    public bool TrySit(ICharacterController newOccupant)
    {
        return seat.TrySit(newOccupant);
    }

    public bool TryStand(out ICharacterController dismissedOccupant)
    {
        dismissedOccupant = Stand();
        return dismissedOccupant != null;
    }

    public CharacterSeat Seat
    {
        get { return seat; }
    }

    private ICharacterController Stand()
    {
        if (!seat.Occupied) { return null; }

        if(seat.TryGetOccupant(out ICharacterController occupant))
        {
            occupant.transform.SetParent(null);
            occupant.EnablePhysics(true);
            occupant.transform.SetPositionAndRotation(exitToPosition.position, transform.rotation);
            return occupant;
        }
        else
        {
            return null; // Unoccupied.
        }
    }
}
